namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphNamespaceTests
{
    [Fact]
    public void Equals_ShouldBeEqual()
    {
        // arrange
        var namespace1 = new OpenGraphNamespace("ns1", "https://example.com/ns1#");
        var namespace2 = new OpenGraphNamespace("ns1", "https://example.com/ns2#");

        // act
        var result = namespace1.Equals(namespace2);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldNotBeEqual()
    {
        // arrange
        var namespace1 = new OpenGraphNamespace("ns1", "https://example.com/ns1#");
        var namespace2 = new OpenGraphNamespace("ns2", "https://example.com/ns1#");

        // act
        var result = namespace1.Equals(namespace2);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OpenGraph_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraph;

        // assert
        ns.Prefix.Should().Be("og");
        ns.SchemaUri.Should().Be("https://ogp.me/ns#");
    }

    [Fact]
    public void OpenGraphVideo_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraphVideo;

        // assert
        ns.Prefix.Should().Be("video");
        ns.SchemaUri.Should().Be("https://ogp.me/ns/video#");
    }

    [Fact]
    public void OpenGraphMusic_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraphMusic;

        // assert
        ns.Prefix.Should().Be("music");
        ns.SchemaUri.Should().Be("https://ogp.me/ns/music#");
    }

    [Fact]
    public void OpenGraphArticle_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraphArticle;

        // assert
        ns.Prefix.Should().Be("article");
        ns.SchemaUri.Should().Be("https://ogp.me/ns/article#");
    }

    [Fact]
    public void OpenGraphBook_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraphBook;

        // assert
        ns.Prefix.Should().Be("book");
        ns.SchemaUri.Should().Be("https://ogp.me/ns/book#");
    }

    [Fact]
    public void OpenGraphProfile_ShouldContainProperties()
    {
        // act
        var ns = OpenGraphNamespace.OpenGraphProfile;

        // assert
        ns.Prefix.Should().Be("profile");
        ns.SchemaUri.Should().Be("https://ogp.me/ns/profile#");
    }
}