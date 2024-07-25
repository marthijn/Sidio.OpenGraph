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

        var openGraph = (OpenGraph)controller.ViewData[Constants.ViewDataKey]!;
        var tags = openGraph.MetaTags.Select(x => x.Property).ToList();
        tags.Should().Contain("og:title");
        tags.Should().Contain("og:type");
        tags.Should().Contain("og:image");
        tags.Should().Contain("og:url");

        tags.Should().NotContain("og:description");
        tags.Should().NotContain("og:locale");
        tags.Should().NotContain("og:site_name");
    }

    [Fact]
    public void SetOpenGraph_WithAllParameters_ShouldSetOpenGraph()
    {
        // arrange
        var controller = new TestController();
        var additionalTags = new HashSet<OpenGraphMetaTag>
        {
            new("audio", "test"),
        };

        // act
        controller.SetOpenGraph(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            additionalTags);

        // assert
        controller.ViewData[Constants.ViewDataKey].Should().NotBeNull().And.BeOfType<OpenGraph>();

        var openGraph = (OpenGraph)controller.ViewData[Constants.ViewDataKey]!;
        var tags = openGraph.MetaTags.Select(x => x.Property).ToList();
        tags.Should().Contain("og:title");
        tags.Should().Contain("og:type");
        tags.Should().Contain("og:image");
        tags.Should().Contain("og:url");

        tags.Should().Contain("og:description");
        tags.Should().Contain("og:locale");
        tags.Should().Contain("og:site_name");

        tags.Should().Contain("og:audio");
    }

    private sealed class TestController : Controller;
}