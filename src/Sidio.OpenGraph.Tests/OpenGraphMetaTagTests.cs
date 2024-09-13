namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphMetaTagTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithNameAndContent_ShouldBeConstructed()
    {
        // arrange
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();

        // act
        var meta = new OpenGraphMetaTag(propertyName, content);

        // assert
        meta.PropertyName.Should().Be(propertyName);
        meta.Content.Should().Be(content);
        meta.Namespace.Should().BeEquivalentTo(OpenGraphNamespace.OpenGraph);
    }

    [Fact]
    public void Construct_WithNameAndContentAndNamespace_ShouldBeConstructed()
    {
        // arrange
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var ns = _fixture.Create<OpenGraphNamespace>();

        // act
        var meta = new OpenGraphMetaTag(propertyName, content, ns);

        // assert
        meta.PropertyName.Should().Be(propertyName);
        meta.Content.Should().Be(content);
        meta.Namespace.Should().BeEquivalentTo(ns);
    }

    [Fact]
    public void Construct_WithNameAndContentAndStructuredName_ShouldBeConstructed()
    {
        // arrange
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var structuredPropertyName = _fixture.Create<string>();

        // act
        var meta = new OpenGraphMetaTag(propertyName, structuredPropertyName, content);

        // assert
        meta.PropertyName.Should().Be(propertyName);
        meta.Content.Should().Be(content);
        meta.Namespace.Should().BeEquivalentTo(OpenGraphNamespace.OpenGraph);
        meta.StructuredPropertyName.Should().Be(structuredPropertyName);
    }

    [Fact]
    public void Construct_WithNameAndContentAndStructuredNameAndNamespace_ShouldBeConstructed()
    {
        // arrange
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var structuredPropertyName = _fixture.Create<string>();
        var ns = _fixture.Create<OpenGraphNamespace>();

        // act
        var meta = new OpenGraphMetaTag(propertyName, structuredPropertyName, content, ns);

        // assert
        meta.PropertyName.Should().Be(propertyName);
        meta.Content.Should().Be(content);
        meta.Namespace.Should().BeEquivalentTo(ns);
        meta.StructuredPropertyName.Should().Be(structuredPropertyName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_PropertyNameIsNull_ShouldThrowException(string? propertyName)
    {
        // arrange
        var content = _fixture.Create<string>();

        // act
        var action = () => new OpenGraphMetaTag(propertyName!, content);

        // assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("", "test")]
    [InlineData(" ", "test")]
    [InlineData(null, "test")]
    [InlineData("test", " ")]
    [InlineData("test", "  ")]
    [InlineData("test", null)]
    public void Construct_StructuredPropertyNameIsNull_ShouldThrowException(string? propertyName, string? structuredPropertyName)
    {
        // arrange
        var content = _fixture.Create<string>();

        // act
        var action = () => new OpenGraphMetaTag(propertyName!, structuredPropertyName!, content);

        // assert
        action.Should().Throw<ArgumentException>();
    }

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

    [Fact]
    public void Equals_NullReferences_ShouldNotBeEqual()
    {
        // arrange
        var meta1 = new OpenGraphMetaTag(
            "property1",
            "content1",
            new OpenGraphNamespace(_fixture.Create<string>(), _fixture.Create<string>()));
        OpenGraphMetaTag? meta2 = null;

        // act
        var result = meta1.Equals(meta2!);

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