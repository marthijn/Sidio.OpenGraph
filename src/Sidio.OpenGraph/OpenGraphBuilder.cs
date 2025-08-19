using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace Sidio.OpenGraph;

/// <summary>
/// The OpenGraphBuilder class.
/// </summary>
public sealed class OpenGraphBuilder : IOpenGraphBuilder
{
    private readonly ILogger<OpenGraphBuilder>? _logger;
    private readonly HashSet<OpenGraphMetaTag> _metaTags = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphBuilder"/> class.
    /// </summary>
    public OpenGraphBuilder()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphBuilder"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public OpenGraphBuilder(ILogger<OpenGraphBuilder> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
#if NETSTANDARD2_0
    public HashSet<OpenGraphMetaTag> MetaTags  => _metaTags;
#else
    public IReadOnlySet<OpenGraphMetaTag> MetaTags  => _metaTags;
#endif

#if NET6_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.MemberNotNullWhen(true, nameof(_logger))]
#endif
    private bool TraceLoggingEnabled => _logger?.IsEnabled(LogLevel.Trace) ?? false;

    internal IEnumerable<OpenGraphNamespace> Namespaces => _metaTags.Select(x => x.Namespace).Distinct();

    /// <inheritdoc />
    public bool Add(OpenGraphMetaTag openGraphMetaTag)
    {
        var result = _metaTags.Add(openGraphMetaTag);

        if (TraceLoggingEnabled)
        {
            _logger!.LogTrace(
                "Added meta tag {Prefix}:{MetaTag}: {AddResult}",
                openGraphMetaTag.Namespace.Prefix,
                openGraphMetaTag.PropertyName,
                result);
        }

        return result;
    }

    /// <inheritdoc />
    public bool Add(string propertyName, string? content) => Add(new OpenGraphMetaTag(propertyName, content));

    /// <inheritdoc />
    public bool Add(string propertyName, string? content, OpenGraphNamespace openGraphNamespace) =>
        Add(new OpenGraphMetaTag(propertyName, content, openGraphNamespace));

    /// <inheritdoc />
    public void AddRange(IEnumerable<OpenGraphMetaTag> openGraphMetas)
    {
        foreach (var openGraphMeta in openGraphMetas)
        {
            Add(openGraphMeta);
        }
    }

    /// <inheritdoc />
    public string GetPrefixAttributeValue()
    {
        var pool = ObjectPool.Create<StringBuilder>();
        var sb = pool.Get();

        try
        {
            foreach (var ns in Namespaces)
            {
                sb.Append($"{ns.Prefix}: {ns.SchemaUri} ");
            }

            return sb.ToString().Trim();
        }
        finally
        {
            pool.Return(sb);
        }
    }

    /// <inheritdoc />
    public void Validate()
    {
        foreach (var ns in Namespaces.Where(x => x.RequiredProperties.Count > 0))
        {
            var properties = GetMetaTagsByNamespace(ns).ToList();
            foreach (var requiredProperty in ns.RequiredProperties)
            {
                if (!properties.Any(x => x.PropertyName.Equals(requiredProperty)))
                {
                    throw new OpenGraphValidationException(
                        $"Missing required property {requiredProperty} in namespace {ns.Prefix}");
                }
            }
        }
    }

    /// <inheritdoc />
    public OpenGraph Build()
    {
        Validate();
        return new OpenGraph(GetPrefixAttributeValue(), MetaTags);
    }

    private IEnumerable<OpenGraphMetaTag> GetMetaTagsByNamespace(OpenGraphNamespace ns) =>
        _metaTags.Where(x => x.Namespace.Equals(ns));
}