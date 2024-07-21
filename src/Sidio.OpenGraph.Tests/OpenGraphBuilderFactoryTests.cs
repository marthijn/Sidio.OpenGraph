using Microsoft.Extensions.Logging;

namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphBuilderFactoryTests
{
    [Fact]
    public void Create_WhenCalled_ReturnsNewInstance()
    {
        // arrange
        var loggerFactory = new LoggerFactory();
        var factory = new OpenGraphBuilderFactory(loggerFactory);

        // act
        var result = factory.Create();

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OpenGraphBuilder>();
    }
}