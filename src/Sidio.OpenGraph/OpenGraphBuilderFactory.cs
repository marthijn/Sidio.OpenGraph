using System.Text;
using Microsoft.Extensions.Logging;
using Sidio.ObjectPool;

namespace Sidio.OpenGraph;

internal sealed class OpenGraphBuilderFactory : IOpenGraphBuilderFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IObjectPoolService<StringBuilder> _objectPoolService;

    public OpenGraphBuilderFactory(ILoggerFactory loggerFactory, IObjectPoolService<StringBuilder> objectPoolService)
    {
        _loggerFactory = loggerFactory;
        _objectPoolService = objectPoolService;
    }

    public IOpenGraphBuilder Create()
    {
        return new OpenGraphBuilder(_objectPoolService, _loggerFactory.CreateLogger<OpenGraphBuilder>());
    }
}