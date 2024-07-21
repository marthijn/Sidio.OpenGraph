namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphMetaTagTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Equals_ShouldBeEqual()
    {
        // arrange
        var meta1 = new OpenGraphMetaTag("property1", _fixture.Create<string>(), OpenGraphNamespace.OpenGraphMusic);
        var meta2 = new OpenGraphMetaTag("property1", _fixture.Create<string>(), OpenGraphNamespace.OpenGraphMusic);

        // act
        var result = meta1.Equals(meta2);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentProperty_ShouldNotBeEqual()
    {
        // arrange
        var meta1 = new OpenGraphMetaTag(_fixture.Create<string>(), "content1");
        var meta2 = new OpenGraphMetaTag(_fixture.Create<string>(), "content1");

        // act
        var result = meta1.Equals(meta2);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_DifferentNamespace_ShouldNotBeEqual()
    {
        // arrange
        var meta1 = new OpenGraphMetaTag(
            "property1",
            "content1",
            new OpenGraphNamespace(_fixture.Create<string>(), _fixture.Create<string>()));
        var meta2 = new OpenGraphMetaTag(
            "property1",
            "content1",
            new OpenGraphNamespace(_fixture.Create<string>(), _fixture.Create<string>()));

        // act
        var result = meta1.Equals(meta2);

        // assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("property1", "", "abc:property1")]
    [InlineData("property1", "structuredProperty1", "abc:property1:structuredProperty1")]
    public void Property_ShouldReturnProperty(string propertyName, string? structuredPropertyName, string expected)
    {
        // arrange
        var ns = new OpenGraphNamespace("abc", "def");
        var meta = structuredPropertyName != null && !string.IsNullOrWhiteSpace(structuredPropertyName)
            ? new OpenGraphMetaTag(
                propertyName,
                structuredPropertyName,
                "content1",
                ns)
            : new OpenGraphMetaTag(propertyName, "content1", ns);

        // act
        var result = meta.Property;

        // assert
        result.Should().Be(expected);
    }
}