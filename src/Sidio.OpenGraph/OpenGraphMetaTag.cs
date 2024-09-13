namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph meta tag class.
/// </summary>
public sealed record OpenGraphMetaTag : IEqualityComparer<OpenGraphMetaTag>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphMetaTag"/> class.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="content">The content.</param>
    /// <exception cref="ArgumentException">Thrown when the propertyName is null or white space.</exception>
    public OpenGraphMetaTag(string propertyName, string? content)
        : this(propertyName, content, OpenGraphNamespace.OpenGraph)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphMetaTag"/> class.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="content">The content.</param>
    /// <param name="openGraphNamespace">The namespace.</param>
    /// <exception cref="ArgumentException">Thrown when the propertyName is null or white space.</exception>
    public OpenGraphMetaTag(string propertyName, string? content, OpenGraphNamespace openGraphNamespace)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyName));
        }

        PropertyName = propertyName;
        Content = content;
        Namespace = openGraphNamespace;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphMetaTag"/> class.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="structuredPropertyName">The structured property name.</param>
    /// <param name="content">The content.</param>
    /// <exception cref="ArgumentException">Thrown when the propertyName or structuredPropertyName is null or white space.</exception>
    public OpenGraphMetaTag(string propertyName, string structuredPropertyName, string? content)
        : this(propertyName, structuredPropertyName, content, OpenGraphNamespace.OpenGraph)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphMetaTag"/> class.
    /// </summary>
    /// <param name="propertyName">The property name without the prefix.</param>
    /// <param name="structuredPropertyName">The structured property name.</param>
    /// <param name="content">The content.</param>
    /// <param name="openGraphNamespace">The namespace.</param>
    /// <exception cref="ArgumentException">Thrown when the propertyName or structuredPropertyName is null or white space.</exception>
    public OpenGraphMetaTag(string propertyName, string structuredPropertyName, string? content, OpenGraphNamespace openGraphNamespace)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyName));
        }

        if (string.IsNullOrWhiteSpace(structuredPropertyName))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(structuredPropertyName));
        }

        PropertyName = propertyName;
        StructuredPropertyName = structuredPropertyName;
        Content = content;
        Namespace = openGraphNamespace;
    }

    /// <summary>
    /// Gets the property name without the namespace.
    /// </summary>
    internal string PropertyName { get;  }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string? Content { get;  }

    /// <summary>
    /// Gets the namespace.
    /// </summary>
    public OpenGraphNamespace Namespace { get; }

    /// <summary>
    /// Gets the structured property name.
    /// </summary>
    internal string? StructuredPropertyName { get; }

    /// <summary>
    /// Gets the property value (including the namespace, i.e. "namespace:property-name").
    /// </summary>
    public string Property => !string.IsNullOrWhiteSpace(StructuredPropertyName)
        ? $"{Namespace.Prefix}:{PropertyName}:{StructuredPropertyName}"
        : $"{Namespace.Prefix}:{PropertyName}";

    /// <inheritdoc />
    public bool Equals(OpenGraphMetaTag? other) => Equals(this, other);

    /// <inheritdoc />
    public override int GetHashCode() => GetHashCode(this);

    /// <inheritdoc />
    public bool Equals(OpenGraphMetaTag? x, OpenGraphMetaTag? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Property == y.Property;
    }

    /// <inheritdoc />
    public int GetHashCode(OpenGraphMetaTag obj)
    {
        return HashCode.Combine(obj.Property);
    }
}