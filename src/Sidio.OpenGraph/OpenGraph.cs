namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph object.
/// </summary>
public sealed record OpenGraph
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraph"/> class.
    /// </summary>
    /// <param name="prefixAttributeValue"></param>
    /// <param name="metaTags"></param>
#if NETSTANDARD2_0
    public OpenGraph(string prefixAttributeValue, HashSet<OpenGraphMetaTag> metaTags)
#else
    public OpenGraph(string prefixAttributeValue, IReadOnlySet<OpenGraphMetaTag> metaTags)
#endif
    {
        PrefixAttributeValue = prefixAttributeValue;
        MetaTags = metaTags;
    }

    /// <summary>
    /// Gets the prefix attribute value.
    /// </summary>
    public string PrefixAttributeValue { get;  }

    /// <summary>
    /// Gets the Open Graph meta tags.
    /// </summary>
#if NETSTANDARD2_0
    public HashSet<OpenGraphMetaTag> MetaTags { get; }
#else
    public IReadOnlySet<OpenGraphMetaTag> MetaTags { get; }
#endif
}