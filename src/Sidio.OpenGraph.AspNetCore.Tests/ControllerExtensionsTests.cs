using Microsoft.AspNetCore.Mvc;

namespace Sidio.OpenGraph.AspNetCore.Tests;

public sealed class ControllerExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void SetOpenGraph_WithOpenGraphObject_ShouldSetOpenGraph()
    {
        // arrange
        var controller = new TestController();
        var openGraph = new OpenGraphBuilder().Build();

        // act
        controller.SetOpenGraph(openGraph);

        // assert
        controller.ViewData[Constants.ViewDataKey].Should().Be(openGraph);
    }

    [Fact]
    public void SetOpenGraph_WithParameters_ShouldSetOpenGraph()
    {
        // arrange
        var controller = new TestController();

        // act
        controller.SetOpenGraph(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>());

        // assert
        controller.ViewData[Constants.ViewDataKey].Should().NotBeNull().And.BeOfType<OpenGraph>();
    }

    private sealed class TestController : Controller;
}