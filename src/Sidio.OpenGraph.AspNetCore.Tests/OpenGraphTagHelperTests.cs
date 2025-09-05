using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.ObjectPool;
using Sidio.ObjectPool;

namespace Sidio.OpenGraph.AspNetCore.Tests;

public sealed class OpenGraphTagHelperTests
{
    [Fact]
    public void Process_NoOpenGraphData_ReturnsNothing()
    {
        // Arrange
        var tagHelper = new OpenGraphTagHelper(CreateObjectPoolService())
        {
            ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext()
        };

        var context = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            Guid.NewGuid().ToString("N"));
        var output = new TagHelperOutput(
            "head",
            new TagHelperAttributeList(),
            (_, _) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });

        // Act
        tagHelper.Process(context, output);

        // Assert
        output.Content.GetContent().Should().BeEmpty();
    }

    [Fact]
    public void Process_WithOpenGraphData_ReturnsOpenGraph()
    {
        // Arrange
        var openGraphMetaTags = new HashSet<OpenGraphMetaTag> {new("title", "Test Title")};
        var openGraph = new OpenGraph("prefix", openGraphMetaTags);
        var viewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
        {
            ViewData =
            {
                ["Sidio.OpenGraph.Data"] = openGraph
            }
        };

        var tagHelper = new OpenGraphTagHelper(CreateObjectPoolService())
        {
            ViewContext = viewContext
        };

        var context = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            Guid.NewGuid().ToString("N"));
        var output = new TagHelperOutput(
            "head",
            new TagHelperAttributeList(),
            (_, _) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });

        // Act
        tagHelper.Process(context, output);

        // Assert
        var content = output.Content.GetContent();
        content.Should().NotBeEmpty();
        content.Should().Contain("<meta property=\"og:title\" content=\"Test Title\" />");

    }

    private static IObjectPoolService<StringBuilder> CreateObjectPoolService()
    {
        var poolMock = new Mock<ObjectPool<StringBuilder>>();
        poolMock.Setup(x => x.Get()).Returns(() => new StringBuilder());

        var mock = new Mock<IObjectPoolService<StringBuilder>>();
        mock.SetupGet(x => x.Pool).Returns(poolMock.Object);
        return mock.Object;
    }
}