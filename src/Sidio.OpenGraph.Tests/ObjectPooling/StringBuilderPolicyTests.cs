using System.Text;
using Microsoft.Extensions.ObjectPool;
using Sidio.OpenGraph.ObjectPooling;

namespace Sidio.OpenGraph.Tests.ObjectPooling;

public sealed class StringBuilderPolicyTests
{
    [Fact]
    public void Create_ShouldReturnNewInstance()
    {
        // Arrange
        const int Capacity = 20;
        var policy = new StringBuilderPolicy(Capacity);

        // Act
        var result = policy.Create();

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(0);
        result.Capacity.Should().Be(Capacity);
    }

    [Fact]
    public void Return_ShouldClearStringBuilder()
    {
        // Arrange
        var policy = new StringBuilderPolicy();
        var stringBuilder = policy.Create();
        stringBuilder.Append("Test");

        // Act
        var result = policy.Return(stringBuilder);

        // Assert
        stringBuilder.Length.Should().Be(0);
        result.Should().BeTrue();
    }

    [Fact]
    public void Return_WhenCapacityExceeded_DiscardStringBuilder()
    {
        // Arrange
        const int MaxCapacity = 100;
        var policy = new StringBuilderPolicy(10, MaxCapacity);
        var stringBuilder = policy.Create();
        stringBuilder.Append('a', MaxCapacity + 1);

        // Act
        var result = policy.Return(stringBuilder);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Pool_WhenCapacityNotExceeded_UsesPolicyCorrectly()
    {
        // Arrange
        var pool = new DefaultObjectPool<StringBuilder>(new StringBuilderPolicy());

        // Act
        var sb1 = pool.Get();
        sb1.Append("Hello");
        pool.Return(sb1);

        var sb2 = pool.Get(); // should be same instance but cleared

        // Assert
        sb1.Should().BeSameAs(sb2);
        sb2.Length.Should().Be(0);
    }

    [Fact]
    public void Pool_WhenCapacityExceeded_UsesPolicyCorrectly()
    {
        // Arrange
        const int MaxCapacity = 100;
        var pool = new DefaultObjectPool<StringBuilder>(new StringBuilderPolicy(10, MaxCapacity));

        // Act
        var sb1 = pool.Get();
        sb1.Append('a', MaxCapacity + 1);
        pool.Return(sb1);

        var sb2 = pool.Get(); // should be new instance

        // Assert
        sb1.Should().NotBeSameAs(sb2);
        sb2.Length.Should().Be(0);
    }
}