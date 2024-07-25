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
    /// <param name="title">The title of the object as it should appear within the graph.</param>
    /// <param name="type">The type of the object, e.g., "video.movie". Depending on the type specified, other properties may also be required.</param>
    /// <param name="image">An image URL which should represent the object within the graph.</param>
    /// <param name="url">The canonical URL of the object that will be used as its permanent ID in the graph.</param>
    /// <param name="siteName">If the object is part of a larger web site, the name which should be displayed for the overall site. e.g., "IMDb".</param>
    /// <param name="additionalTags">Additional tags that represent the object.</param>
    /// <param name="builder">The builder (optional).</param>
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
        IReadOnlyCollection<OpenGraphMetaTag>? additionalTags = null,
        IOpenGraphBuilder? builder = null)
    {
        builder ??= new OpenGraphBuilder();
        builder.Add("title", title);
        builder.Add("type", type);
        builder.Add("image", image);
        builder.Add("url", url);

        if (description is not null)
        {
            builder.Add("description", description);
        }

        if (locale is not null)
        {
            builder.Add("locale", locale);
        }

        if (siteName is not null)
        {
            builder.Add("site_name", siteName);
        }

        if (additionalTags is {Count: > 0})
        {
            builder.AddRange(additionalTags);
        }

        controller.SetOpenGraph(builder.Build());
    }
}