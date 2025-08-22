using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Sidio.OpenGraph.ObjectPooling;

internal sealed class StringBuilderPolicy : PooledObjectPolicy<StringBuilder>
{
    private readonly int _initialCapacity;
    private readonly int _maxRetainedCapacity;

    public StringBuilderPolicy(int initialCapacity = 256, int maxRetainedCapacity = 1024)
    {
        _initialCapacity = initialCapacity;
        _maxRetainedCapacity = maxRetainedCapacity;
    }

    public override StringBuilder Create() => new (_initialCapacity);

    public override bool Return(StringBuilder sb)
    {
        if (sb.Capacity > _maxRetainedCapacity)
        {
            // too big, discard it and let GC collect it
            return false;
        }

        sb.Clear(); // reset contents for next caller
        return true;
    }
}