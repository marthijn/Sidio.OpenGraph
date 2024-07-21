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
}