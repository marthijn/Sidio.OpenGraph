namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph namespace.
/// </summary>
public sealed partial record OpenGraphNamespace : IEqualityComparer<OpenGraphNamespace>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphNamespace"/> class.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="schemaUri">The schema uri.</param>
    public OpenGraphNamespace(string prefix, string schemaUri)
        : this(prefix, schemaUri, Array.Empty<string>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphNamespace"/> class.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="schemaUri">The schema uri.</param>
    /// <param name="requiredProperties">The required properties for this namespace.</param>
    public OpenGraphNamespace(string prefix, string schemaUri, IEnumerable<string> requiredProperties)
    {
        Prefix = prefix;
        SchemaUri = schemaUri;
        RequiredProperties = requiredProperties.ToList();
    }

    /// <summary>
    /// Gets the prefix.
    /// </summary>
    public string Prefix { get; }

    /// <summary>
    /// Gets the schema uri.
    /// </summary>
    public string SchemaUri { get; }

    /// <summary>
    /// Gets the required properties for this namespace.
    /// </summary>
    public IReadOnlyCollection<string> RequiredProperties { get; }

    /// <inheritdoc />
    public bool Equals(OpenGraphNamespace? other) => Equals(this, other);

    /// <inheritdoc />
    public override int GetHashCode() => GetHashCode(this);

    /// <inheritdoc />
    public bool Equals(OpenGraphNamespace? x, OpenGraphNamespace? y)
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

        return x.Prefix == y.Prefix;
    }

    /// <inheritdoc />
    public int GetHashCode(OpenGraphNamespace obj) => obj.Prefix.GetHashCode();
}