namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphExtensionsTests
{
    [Fact]
    public void MetaTagsToHtml_ReturnsHtmlString()
    {
        // arrange
        var openGraphBuilder = new OpenGraphBuilder();
        openGraphBuilder.Add(new OpenGraphMetaTag("title", "test"));
        openGraphBuilder.Add(new OpenGraphMetaTag("type", "test"));
        openGraphBuilder.Add(new OpenGraphMetaTag("url", "test"));
        openGraphBuilder.Add(new OpenGraphMetaTag("image", "test"));
        openGraphBuilder.Add(new OpenGraphMetaTag("test", "website", new OpenGraphNamespace("ab", "def")));
        var openGraph = openGraphBuilder.Build();

        // act
        var result = openGraph.MetaTagsToHtml();

        // assert
        result.Should().Be(
            "<meta property=\"og:title\" content=\"test\" /><meta property=\"og:type\" content=\"test\" /><meta property=\"og:url\" content=\"test\" /><meta property=\"og:image\" content=\"test\" /><meta property=\"ab:test\" content=\"website\" />");
    }
}