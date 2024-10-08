﻿using System.Text;

namespace Sidio.OpenGraph;

/// <summary>
/// The Open Graph extensions class.
/// </summary>
public static class OpenGraphExtensions
{
    /// <summary>
    /// Converts the Open Graph object to HTML.
    /// </summary>
    /// <param name="openGraph">The Open Graph object.</param>
    /// <returns>A HTML <see cref="string"/>.</returns>
    public static string MetaTagsToHtml(this OpenGraph openGraph)
    {
        var builder = new StringBuilder();
        foreach (var property in openGraph.MetaTags)
        {
            builder.Append($"<meta property=\"{property.Property}\" content=\"{property.Content}\" />");
        }

        return builder.ToString();
    }
}