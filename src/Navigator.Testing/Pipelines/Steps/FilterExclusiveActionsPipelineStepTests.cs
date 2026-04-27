using FluentAssertions;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Pipelines.Steps;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Pipelines.Steps;

public class FilterExclusiveActionsPipelineStepTests
{
    private readonly FilterExclusiveActionsPipelineStep _step;

    public FilterExclusiveActionsPipelineStepTests()
    {
        var tracer = Substitute.For<INavigatorTracer>();
        var tracerFactory = Substitute.For<INavigatorTracerFactory<FilterExclusiveActionsPipelineStep>>();
        tracerFactory.Get(Arg.Any<string?>()).Returns(tracer);
        tracerFactory.HasActiveTrace().Returns(false);

        _step = new FilterExclusiveActionsPipelineStep(
            Substitute.For<ILogger<FilterExclusiveActionsPipelineStep>>(),
            tracerFactory);
    }

    private static BotAction MakeAction(string kind, string? subkind, EExclusivityLevel level)
    {
        return new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = new UpdateCategory(kind, subkind),
                Priority = Abstractions.Priorities.EPriority.Normal,
                ExclusivityLevel = level,
                ChatAction = null,
                ConditionInputTypes = [],
                HandlerInputTypes = [],
                Name = $"{kind}.{subkind}:{level}",
                Options = []
            }, () => true, () => Task.CompletedTask);
    }

    [Fact]
    public async Task ShouldKeepOnlyGlobalAction_WhenHighestPriorityIsGlobal()
    {
        var global = MakeAction("Msg", "Text", EExclusivityLevel.Global);
        var second = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var third = MakeAction("Msg", "Text", EExclusivityLevel.None);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([global, second, third]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().ContainSingle()
            .Which.Should().Be(global);
    }

    [Fact]
    public async Task ShouldRemoveLowerPriorityExclusiveActionsInSameCategory()
    {
        var first = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var second = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var third = MakeAction("Msg", "Text", EExclusivityLevel.Category);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([first, second, third]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().ContainSingle()
            .Which.Should().Be(first);
    }

    [Fact]
    public async Task ShouldFilterCategoriesIndependently()
    {
        var a1 = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var a2 = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var b1 = MakeAction("Msg", "Photo", EExclusivityLevel.Category);
        var b2 = MakeAction("Msg", "Photo", EExclusivityLevel.Category);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([a1, a2, b1, b2]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().HaveCount(2);
        context.Actions.Should().Contain(a1);
        context.Actions.Should().Contain(b1);
    }

    [Fact]
    public async Task ShouldKeepNonExclusiveActions()
    {
        var exclusive = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var nonExclusive = MakeAction("Msg", "Text", EExclusivityLevel.None);
        var anotherExclusive = MakeAction("Msg", "Text", EExclusivityLevel.Category);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([exclusive, nonExclusive, anotherExclusive]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().HaveCount(2);
        context.Actions.Should().Contain(exclusive);
        context.Actions.Should().Contain(nonExclusive);
        context.Actions.Should().NotContain(anotherExclusive);
    }

    [Fact]
    public async Task ShouldNotFilterWhenAllActionsAreNonExclusive()
    {
        var a = MakeAction("Msg", "Text", EExclusivityLevel.None);
        var b = MakeAction("Msg", "Text", EExclusivityLevel.None);
        var c = MakeAction("Cmd", null, EExclusivityLevel.None);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([a, b, c]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().HaveCount(3);
    }

    [Fact]
    public async Task ShouldDiscardGlobalActions_WhenNotHighestPriority()
    {
        var first = MakeAction("Msg", "Text", EExclusivityLevel.Category);
        var globalLower = MakeAction("Msg", "Photo", EExclusivityLevel.Global);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.AddRange([first, globalLower]);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().ContainSingle()
            .Which.Should().Be(first);
    }

    [Fact]
    public async Task ShouldNotFilterSingleAction()
    {
        var sole = MakeAction("Msg", "Text", EExclusivityLevel.Global);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.Add(sole);

        await _step.InvokeAsync(context, () => Task.CompletedTask);

        context.Actions.Should().ContainSingle()
            .Which.Should().Be(sole);
    }

    [Fact]
    public async Task ShouldAlwaysCallNext()
    {
        var called = false;

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));

        await _step.InvokeAsync(context, () =>
        {
            called = true;
            return Task.CompletedTask;
        });

        called.Should().BeTrue();
    }
}
