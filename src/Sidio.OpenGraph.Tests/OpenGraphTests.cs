namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithPrefixAndMetaTags_ShouldBeConstructed()
    {
        // arrange
        var prefix = _fixture.Create<string>();
        var metaTags = _fixture.CreateMany<OpenGraphMetaTag>().ToHashSet();

        // act
        var openGraph = new OpenGraph(prefix, metaTags);

        // assert
        openGraph.PrefixAttributeValue.Should().Be(prefix);
        openGraph.MetaTags.Should().BeEquivalentTo(metaTags);
    }

    [Fact]
    public void EqualityOperator_ShouldBeEqual()
    {
        // Arrange
        var prefix = _fixture.Create<string>();
        var metaTags = _fixture.CreateMany<OpenGraphMetaTag>().ToHashSet();

        var openGraph1 = new OpenGraph(prefix, metaTags);
        var openGraph2 = new OpenGraph(prefix, metaTags);

        // Act
        var result = openGraph1 == openGraph2;

        // Assert
        result.Should().BeTrue();
    }
}