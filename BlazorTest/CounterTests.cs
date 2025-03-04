using Xunit;
using Bunit;
using NSubstitute;
using FluentAssertions;
using BlazzorApp.Components.Pages;

namespace BlazorTest;

public class CounterTests
{
    [Fact]
    public void Counter_ShouldIncrement_WhenButtonClicked()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Counter>();

        // Act
        cut.Find("button").Click();

        // Assert
        cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");
    }
}