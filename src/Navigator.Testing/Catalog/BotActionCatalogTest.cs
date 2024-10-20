using FluentAssertions;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Priorities;
using Navigator.Catalog;
using Xunit;

namespace Navigator.Testing.Catalog;

public class BotActionCatalogTest
{
    [Fact]
    public void ShouldRetrieveActionsByCategory()
    {
        #region Arrange

        var category = new UpdateCategory("Test");
        
        var action1 = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = new UpdateCategory("Other"),
                Priority = EPriority.High,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "High",
                Options = []
            }, () => true, () => Task.CompletedTask);
        var action2 = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = category,
                Priority = EPriority.Normal,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "Normal",
                Options = []
            }, () => true, () => Task.CompletedTask);
        var action3 = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = category,
                Priority = EPriority.Low,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "Low",
                Options = []
            }, () => true, () => Task.CompletedTask);

        #endregion
        
        var catalog = new BotActionCatalog(new List<BotAction> { action1, action2, action3 });

        var retrievedActions = catalog.Retrieve(category);

        retrievedActions.Should().Contain(action2);
        retrievedActions.Should().Contain(action3);
        retrievedActions.Should().NotContain(action1);
    }

    [Fact]
    public void ShouldOrderActionsByPriority()
    {
        #region Arrange

        var highPriorityAction = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = new UpdateCategory("Test"),
                Priority = EPriority.High,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "High",
                Options = []
            }, () => true, () => Task.CompletedTask);
        var normalPriorityAction = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = new UpdateCategory("Test"),
                Priority = EPriority.Normal,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "Normal",
                Options = []
            }, () => true, () => Task.CompletedTask);
        var lowPriorityAction = new BotAction(Guid.NewGuid(),
            new BotActionInformation
            {
                Category = new UpdateCategory("Test"),
                Priority = EPriority.Low,
                ChatAction = null,
                ConditionInputTypes = [],
                Cooldown = null,
                HandlerInputTypes = [],
                Name = "Low",
                Options = []
            }, () => true, () => Task.CompletedTask);

        #endregion

        var actions = new List<BotAction> { lowPriorityAction, highPriorityAction, normalPriorityAction };

        var catalog = new BotActionCatalog(actions);

        // Act
        var retrievedActions = catalog.Retrieve(new UpdateCategory("Test"));

        // Assert
        Assert.Equal(3, retrievedActions.Count());
        Assert.Equal(highPriorityAction.Id, retrievedActions.First().Id);
        Assert.Equal(normalPriorityAction.Id, retrievedActions.ElementAt(1).Id);
        Assert.Equal(lowPriorityAction.Id, retrievedActions.Last().Id);
    }

    [Fact]
    public void ShouldHandleEmptyCatalog()
    {
        // Arrange
        var catalog = new BotActionCatalog(new List<BotAction>());

        // Act
        var retrievedActions = catalog.Retrieve(new UpdateCategory("Test"));

        // Assert
        Assert.Empty(retrievedActions);
    }
}