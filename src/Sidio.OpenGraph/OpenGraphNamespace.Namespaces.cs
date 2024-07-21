namespace Sidio.OpenGraph;

public sealed partial record OpenGraphNamespace
{
    /// <summary>
    /// The Open Graph namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraph => new(
        "og",
        "https://ogp.me/ns#",
        new[] {"title", "type", "image", "url"});

    /// <summary>
    /// The Open Graph music namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraphMusic => new("music", "https://ogp.me/ns/music#");

    /// <summary>
    /// The Open Graph video namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraphVideo => new("video", "https://ogp.me/ns/video#");

    /// <summary>
    /// The Open Graph article namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraphArticle => new("article", "https://ogp.me/ns/article#");

    /// <summary>
    /// The Open Graph book namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraphBook => new("book", "https://ogp.me/ns/book#");

    /// <summary>
    /// The Open Graph profile namespace.
    /// </summary>
    public static OpenGraphNamespace OpenGraphProfile => new("profile", "https://ogp.me/ns/profile#");
}