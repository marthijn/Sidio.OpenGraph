namespace Sidio.OpenGraph;

/// <summary>
/// The factory for creating instances of the <see cref="IOpenGraphBuilder"/>.
/// </summary>
public interface IOpenGraphBuilderFactory
{
    /// <summary>
    /// Returns a new instance of the <see cref="IOpenGraphBuilder"/>.
    /// </summary>
    /// <returns></returns>
    IOpenGraphBuilder Create();
}