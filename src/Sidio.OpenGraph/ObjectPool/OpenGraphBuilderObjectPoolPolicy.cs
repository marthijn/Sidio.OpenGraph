using Microsoft.Extensions.ObjectPool;

namespace Sidio.OpenGraph.ObjectPool;

/// <summary>
/// This class defines a policy for pooling <see cref="OpenGraphBuilder"/> instances.
/// </summary>
public sealed class OpenGraphBuilderObjectPoolPolicy : PooledObjectPolicy<OpenGraphBuilder>
{
    private readonly IOpenGraphBuilderFactory _factory;
    private readonly int _maximumRetainedCapacity;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphBuilderObjectPoolPolicy"/> class.
    /// </summary>
    /// <param name="factory">The open graph builder factory.</param>
    /// <param name="maximumRetainedCapacity">The maximum retained capacity.</param>
    public OpenGraphBuilderObjectPoolPolicy(IOpenGraphBuilderFactory factory, int maximumRetainedCapacity = 32)
    {
        _factory = factory;
        _maximumRetainedCapacity = maximumRetainedCapacity;
    }

    /// <inheritdoc />
    public override OpenGraphBuilder Create() => _factory.Create();

    /// <inheritdoc />
    public override bool Return(OpenGraphBuilder obj)
    {
        if (obj.MetaTags.Count > _maximumRetainedCapacity)
        {
            return false;
        }

        obj.Clear();
        return true;
    }
}