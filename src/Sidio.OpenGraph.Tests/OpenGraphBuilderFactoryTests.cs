using System.Text;
using Microsoft.Extensions.Logging;
using Sidio.ObjectPool;

namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphBuilderFactoryTests
{
    [Fact]
    public void Create_WhenCalled_ReturnsNewInstance()
    {
        // arrange
        var loggerFactory = new LoggerFactory();
        var objectPoolService = new Mock<IObjectPoolService<StringBuilder>>();

        var factory = new OpenGraphBuilderFactory(loggerFactory, objectPoolService.Object);

        // act
        var result = factory.Create();

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OpenGraphBuilder>();
    }
}