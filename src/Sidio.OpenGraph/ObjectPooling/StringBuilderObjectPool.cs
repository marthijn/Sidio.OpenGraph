using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Sidio.OpenGraph.ObjectPooling;

internal static class StringBuilderObjectPool
{
    public static ObjectPool<StringBuilder> Pool { get; } = new DefaultObjectPool<StringBuilder>(new StringBuilderPolicy());
}