namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph builder.
/// </summary>
public interface IOpenGraphBuilder
{
    /// <summary>
    /// Gets the Open Graph meta tags.
    /// </summary>
#if NETSTANDARD2_0
    HashSet<OpenGraphMetaTag> MetaTags { get; }
#else
    IReadOnlySet<OpenGraphMetaTag> MetaTags { get; }
#endif

    /// <summary>
    /// Adds an Open Graph meta tag if it does not exist.
    /// </summary>
    /// <param name="openGraphMetaTag">The open graph meta tag.</param>
    /// <returns>Returns <c>true</c> when the element is added.</returns>
    bool Add(OpenGraphMetaTag openGraphMetaTag);

    /// <summary>
    /// Adds an Open Graph meta tag if it does not exist.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="content">The content.</param>
    /// <returns>Returns <c>true</c> when the element is added.</returns>
    bool Add(string propertyName, string? content);

    /// <summary>
    /// Adds an Open Graph meta tag if it does not exist.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="content">the content.</param>
    /// <param name="openGraphNamespace">the namespace.</param>
    /// <returns>Returns <c>true</c> when the element is added.</returns>
    bool Add(string propertyName, string? content, OpenGraphNamespace openGraphNamespace);

    /// <summary>
    /// Adds a range of Open Graph meta tags.
    /// </summary>
    /// <param name="openGraphMetas">The open graph meta objects.</param>
    void AddRange(IEnumerable<OpenGraphMetaTag> openGraphMetas);

    /// <summary>
    /// Returns the prefix attribute value.
    /// </summary>
    /// <returns></returns>
    string GetPrefixAttributeValue();

    /// <summary>
    /// Validates the Open Graph object.
    /// </summary>
    /// <exception cref="OpenGraphValidationException">Thrown when the Open Graph object is invalid.</exception>
    void Validate();

    /// <summary>
    /// Builds the Open Graph object.
    /// </summary>
    /// <remarks>A validation is performed before building the object.</remarks>
    /// <returns>THe <see cref="OpenGraph"/>.</returns>
    OpenGraph Build();
}