using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sidio.ObjectPool;

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
    [Obsolete("Use the overload that takes an Action<IOpenGraphBuilder> instead.")]
    public static void SetOpenGraph(this Controller controller, OpenGraph openGraph)
    {
        controller.SetOpenGraphInternal(openGraph);
    }

    /// <summary>
    /// Set the Open Graph object in the ViewData.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="builderConfiguration">The build configuration.</param>
    public static void SetOpenGraph(this Controller controller, Action<IOpenGraphBuilder> builderConfiguration)
    {
        var objectPoolService =
            (IObjectPoolService<OpenGraphBuilder>) controller.HttpContext.RequestServices.GetRequiredService(
                typeof(IObjectPoolService<OpenGraphBuilder>));

        var b = objectPoolService.Get();
        try
        {
            builderConfiguration(b);
            var openGraph = b.Build();
            controller.SetOpenGraphInternal(openGraph);
        }
        finally
        {
            objectPoolService.Return(b);
        }
    }

    /// <summary>
    /// Set the Open Graph object in the ViewData.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="title">The title of the object as it should appear within the graph.</param>
    /// <param name="type">The type of the object, e.g., "video.movie". Depending on the type specified, other properties may also be required.</param>
    /// <param name="image">An image URL which should represent the object within the graph.</param>
    /// <param name="url">The canonical URL of the object that will be used as its permanent ID in the graph.</param>
    /// <param name="siteName">If the object is part of a larger web site, the name which should be displayed for the overall site. e.g., "IMDb".</param>
    /// <param name="additionalTags">Additional tags that represent the object.</param>
    /// <param name="description">A one to two sentence description of the object.</param>
    /// <param name="locale">The locale in the format language_TERRITORY.</param>
    public static void SetOpenGraph(
        this Controller controller,
        string title,
        string type,
        string image,
        string url,
        string? description = null,
        string? locale = null,
        string? siteName = null,
        IReadOnlyCollection<OpenGraphMetaTag>? additionalTags = null)
    {
        controller.SetOpenGraph(Build);

        return;

        void Build(IOpenGraphBuilder openGraphBuilder)
        {
            openGraphBuilder.Add("title", title);
            openGraphBuilder.Add("type", type);
            openGraphBuilder.Add("image", image);
            openGraphBuilder.Add("url", url);

            if (description is not null)
            {
                openGraphBuilder.Add("description", description);
            }

            if (locale is not null)
            {
                openGraphBuilder.Add("locale", locale);
            }

            if (siteName is not null)
            {
                openGraphBuilder.Add("site_name", siteName);
            }

            if (additionalTags is {Count: > 0})
            {
                openGraphBuilder.AddRange(additionalTags);
            }

            controller.SetOpenGraphInternal(openGraphBuilder.Build());
        }
    }

    private static void SetOpenGraphInternal(this Controller controller, OpenGraph openGraph)
    {
        controller.ViewData[Constants.ViewDataKey] = openGraph;
    }
}