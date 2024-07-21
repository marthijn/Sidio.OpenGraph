using Microsoft.AspNetCore.Mvc;

namespace Sidio.OpenGraph.AspNetCore;

/// <summary>
/// The controller extensions.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Set the Open Graph object in the ViewData.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="openGraph">The Open Graph object.</param>
    public static void SetOpenGraph(this Controller controller, OpenGraph openGraph)
    {
        controller.ViewData[Constants.ViewDataKey] = openGraph;
    }

    /// <summary>
    /// Set the Open Graph object in the ViewData.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="title">The title.</param>
    /// <param name="type">The type.</param>
    /// <param name="image">The image.</param>
    /// <param name="url">The url.</param>
    /// <param name="builder">The builder (optional).</param>
    public static void SetOpenGraph(
        this Controller controller,
        string title,
        string type,
        string image,
        string url,
        IOpenGraphBuilder? builder = null)
    {
        builder ??= new OpenGraphBuilder();
        builder.Add("title", title);
        builder.Add("type", type);
        builder.Add("image", image);
        builder.Add("url", url);

        controller.SetOpenGraph(builder.Build());
    }
}