using Bunit;
using Xunit;
using NSubstitute;
using FluentAssertions;
using BlazzorApp.Components.Pages;
using BlazzorApp.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTest;

public class WeatherTests
{
    [Fact]
    public void WeatherComponent_ShouldShowLoadingInitially()
    {
        // Arrange
        using var ctx = new TestContext();
        var weatherService = Substitute.For<IWeatherService>();
        ctx.Services.AddSingleton(weatherService);
        var cut = ctx.RenderComponent<Weather>();

        // Act
        var loadingMessage = cut.Find("p em");

        // Assert
        loadingMessage.MarkupMatches("<em>Loading...</em>");
    }

    [Fact]
    public async Task WeatherComponent_ShouldShowTableWithDataAfterLoading()
    {
        // Arrange
        using var ctx = new TestContext();
        var weatherService = Substitute.For<IWeatherService>();
        var mockForecasts = new[]
        {
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" }
        };
        weatherService.GetForecastAsync().Returns(Task.FromResult(mockForecasts));
        ctx.Services.AddSingleton(weatherService);
        var cut = ctx.RenderComponent<Weather>();

        // Act
        cut.WaitForState(() => cut.FindAll("table tbody tr").Count > 0);

        // Assert
        var tableRows = cut.FindAll("table tbody tr");
        tableRows.Count.Should().BeGreaterThan(0);
    }
}
