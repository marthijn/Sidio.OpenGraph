using System.Text;

namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph extensions class.
/// </summary>
public static class OpenGraphExtensions
{
    /// <summary>
    /// Converts the Open Graph object to HTML.
    /// </summary>
    /// <param name="openGraph">The Open Graph object.</param>
    /// <returns>An HTML <see cref="string"/>.</returns>
    public static string MetaTagsToHtml(this OpenGraph openGraph)
    {
        var builder = new StringBuilder();
        return MetaTagsToHtml(builder, openGraph);
    }

    /// <summary>
    /// Converts the Open Graph object to HTML.
    /// </summary>
    /// <param name="openGraph">The Open Graph object.</param>
    /// <param name="pool">The string builder object pool.</param>
    /// <returns>An HTML <see cref="string"/>.</returns>
    public static string MetaTagsToHtml(
        this OpenGraph openGraph,
        Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder> pool)
    {
        var builder = pool.Get();

        try
        {
            return MetaTagsToHtml(builder, openGraph);
        }
        finally
        {
            pool.Return(builder);
        }
    }

    private static string MetaTagsToHtml(StringBuilder builder, OpenGraph openGraph)
    {
        foreach (var property in openGraph.MetaTags)
        {
            builder.Append($"<meta property=\"{property.Property}\" content=\"{property.Content}\" />");
        }

        return builder.ToString();
    }
}