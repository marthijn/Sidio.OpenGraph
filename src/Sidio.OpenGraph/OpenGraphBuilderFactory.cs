using Microsoft.Extensions.Logging;

namespace Sidio.OpenGraph;

internal sealed class OpenGraphBuilderFactory : IOpenGraphBuilderFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public OpenGraphBuilderFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public IOpenGraphBuilder Create()
    {
        return new OpenGraphBuilder(_loggerFactory.CreateLogger<OpenGraphBuilder>());
    }
}