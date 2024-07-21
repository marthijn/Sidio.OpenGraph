using Microsoft.AspNetCore.Mvc;

namespace Sidio.OpenGraph.AspNetCore;

public static class ControllerExtensions
{
    public static void SetOpenGraph(this Controller controller, OpenGraph openGraph)
    {
        controller.ViewData[Constants.ViewDataKey] = openGraph;
    }

    public static void SetOpenGraph(this Controller controller, string title, string type, string image, string url)
    {
        var builder = new OpenGraphBuilder();
        builder.Add(new OpenGraphMetaTag("title", title));
        builder.Add(new OpenGraphMetaTag("type", type));
        builder.Add(new OpenGraphMetaTag("image", image));
        builder.Add(new OpenGraphMetaTag("url", url));

        controller.SetOpenGraph(builder.Build());
    }
}