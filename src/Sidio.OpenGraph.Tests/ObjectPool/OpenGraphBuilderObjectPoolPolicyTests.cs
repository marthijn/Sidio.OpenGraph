using Microsoft.Extensions.ObjectPool;
using Sidio.OpenGraph.ObjectPool;

namespace Sidio.OpenGraph.Tests.ObjectPool;

public sealed class OpenGraphBuilderObjectPoolPolicyTests
{
    [Fact]
    public void Create_ShouldReturnNewInstance()
    {
        // Arrange
        const int MaximumCapacity = 20;
        var policy = CreatePolicy(MaximumCapacity);

        // Act
        var result = policy.Create();

        // Assert
        result.Should().NotBeNull();
        result.MetaTags.Should().BeEmpty();
    }

    [Fact]
    public void Return_ShouldClearStringBuilder()
    {
        // Arrange
        var policy = CreatePolicy();
        var openGraphBuilder = policy.Create();
        openGraphBuilder.Add("title", "Test");

        // Act
        var result = policy.Return(openGraphBuilder);

        // Assert
        openGraphBuilder.MetaTags.Should().BeEmpty();
        result.Should().BeTrue();
    }

    [Fact]
    public void Return_WhenCapacityExceeded_DiscardStringBuilder()
    {
        // Arrange
        const int MaxCapacity = 100;
        var policy = CreatePolicy(MaxCapacity);
        var openGraphBuilder = policy.Create();
        Enumerable.Range(0, MaxCapacity + 1).ToList().ForEach(i => openGraphBuilder.Add("title" + i, "Test" + i));

        // Act
        var result = policy.Return(openGraphBuilder);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Pool_WhenCapacityNotExceeded_UsesPolicyCorrectly()
    {
        // Arrange
        var policy = CreatePolicy();
        var pool = new DefaultObjectPool<OpenGraphBuilder>(policy);

        // Act
        var openGraphBuilder1 = pool.Get();
        openGraphBuilder1.Add("title", "Hello");
        pool.Return(openGraphBuilder1);

        var openGraphBuilder2 = pool.Get(); // should be same instance but cleared

        // Assert
        openGraphBuilder1.Should().BeSameAs(openGraphBuilder2);
        openGraphBuilder2.MetaTags.Should().BeEmpty();
    }

    [Fact]
    public void Pool_WhenCapacityExceeded_UsesPolicyCorrectly()
    {
        // Arrange
        const int MaxCapacity = 100;
        var policy = CreatePolicy(MaxCapacity);
        var pool = new DefaultObjectPool<OpenGraphBuilder>(policy);

        // Act
        var openGraphBuilder1 = pool.Get();
        Enumerable.Range(0, MaxCapacity + 1).ToList().ForEach(i => openGraphBuilder1.Add("test" + i, "Test" + i));
        pool.Return(openGraphBuilder1);

        var openGraphBuilder2 = pool.Get(); // should be same instance but cleared

        // Assert
        openGraphBuilder1.Should().NotBeSameAs(openGraphBuilder2);
        openGraphBuilder2.MetaTags.Should().BeEmpty();
    }

    private static OpenGraphBuilderObjectPoolPolicy CreatePolicy(int maximumCapacity = 150)
    {
        var factoryMock = new Mock<IOpenGraphBuilderFactory>();
        factoryMock.Setup(x => x.Create()).Returns(() => new OpenGraphBuilder());

        return new OpenGraphBuilderObjectPoolPolicy(factoryMock.Object, maximumCapacity);
    }
}