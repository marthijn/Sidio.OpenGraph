namespace Sidio.OpenGraph;

/// <summary>
/// The OpenGraphValidationException class.
/// </summary>
[Serializable]
public sealed class OpenGraphValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphValidationException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public OpenGraphValidationException(string message) : base(message)
    {
    }
}