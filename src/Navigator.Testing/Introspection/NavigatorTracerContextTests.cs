using System.Collections.Concurrent;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Sink;
using Navigator.Introspection;
using NSubstitute;
using Xunit;

namespace Navigator.Testing.Introspection;

public class NavigatorTracerContextTests
{
    private readonly NavigatorTracerContext _context;
    private readonly CollectingSink _sink;
    private readonly NavigatorTracerFactory<NavigatorTracerContextTests> _factory;

    public NavigatorTracerContextTests()
    {
        _context = new NavigatorTracerContext(Substitute.For<ILogger<NavigatorTracerContext>>());
        _sink = new CollectingSink();
        _factory = new NavigatorTracerFactory<NavigatorTracerContextTests>(_sink, _context);
    }

    [Fact]
    public async Task NestedTracers_ShouldLinkChildToParent()
    {
        await using (var root = _factory.Get("request-1"))
        {
            await using (var child = _factory.Get())
            {
                await using var grandchild = _factory.Get();
            }

            await using var sibling = _factory.Get();
        }

        var traces = _sink.Traces.ToList();
        traces.Should().HaveCount(4);

        var root1 = traces.Single(t => t.Identifier == "request-1");
        root1.ParentIdentifier.Should().BeNull();

        var childrenOfRoot = traces.Where(t => t.ParentIdentifier == "request-1").ToList();
        childrenOfRoot.Should().HaveCount(2);

        var grandchildren = traces.Where(t => childrenOfRoot.Any(c => c.Identifier == t.ParentIdentifier)).ToList();
        grandchildren.Should().HaveCount(1);
    }

    [Fact]
    public async Task SiblingTracers_ShouldBothLinkToRoot()
    {
        await using (var root = _factory.Get("request-1"))
        {
            await using (_factory.Get("first"))
            {
            }

            await using (_factory.Get("second"))
            {
            }
        }

        _sink.Traces.Single(t => t.Identifier == "first").ParentIdentifier.Should().Be("request-1");
        _sink.Traces.Single(t => t.Identifier == "second").ParentIdentifier.Should().Be("request-1");
    }

    [Fact]
    public async Task ParallelBranches_ShouldEachSeeTheirOwnParent()
    {
        const int branches = 32;

        await using (var root = _factory.Get("request-1"))
        {
            await Task.WhenAll(Enumerable.Range(0, branches).Select(async branch =>
            {
                // Force the branches to interleave so a shared mutable stack would corrupt.
                await Task.Yield();
                await using var branchTracer = _factory.Get($"branch-{branch}");
                await Task.Delay(branch % 4);
                await using var leafTracer = _factory.Get($"leaf-{branch}");
                await Task.Yield();
            }));
        }

        _sink.Traces.Should().HaveCount(1 + branches * 2);

        foreach (var branch in Enumerable.Range(0, branches))
        {
            _sink.Traces.Single(t => t.Identifier == $"branch-{branch}")
                .ParentIdentifier.Should().Be("request-1", "every branch tracer was created directly under the root");

            _sink.Traces.Single(t => t.Identifier == $"leaf-{branch}")
                .ParentIdentifier.Should().Be($"branch-{branch}", "every leaf tracer was created inside its own branch");
        }
    }

    [Fact]
    public async Task ParallelRootFlows_ShouldNotSeeEachOthersTraces()
    {
        const int flows = 16;

        await Task.WhenAll(Enumerable.Range(0, flows).Select(async flow =>
        {
            await Task.Yield();
            await using var root = _factory.Get($"flow-{flow}");
            await Task.Delay(flow % 4);
            await using var child = _factory.Get($"flow-child-{flow}");
        }));

        foreach (var flow in Enumerable.Range(0, flows))
        {
            _sink.Traces.Single(t => t.Identifier == $"flow-{flow}")
                .ParentIdentifier.Should().BeNull("each flow starts its own root trace");

            _sink.Traces.Single(t => t.Identifier == $"flow-child-{flow}")
                .ParentIdentifier.Should().Be($"flow-{flow}");
        }
    }

    [Fact]
    public async Task OutOfOrderDisposal_ShouldNotThrow()
    {
        var root = _factory.Get("request-1");
        var child = _factory.Get("child");

        // Disposing the root while the child is still active must not throw or pop the child.
        await root.DisposeAsync();

        _context.PeekOrDefault().Should().Be("child");

        await child.DisposeAsync();

        // The mismatched pop was skipped, so the root identifier is still on the stack.
        _context.PeekOrDefault().Should().Be("request-1");
    }

    private sealed class CollectingSink : INavigatorTracerSink
    {
        public ConcurrentBag<NavigatorTrace> Traces { get; } = [];

        public Task Store(NavigatorTrace trace)
        {
            Traces.Add(trace);
            return Task.CompletedTask;
        }
    }
}
