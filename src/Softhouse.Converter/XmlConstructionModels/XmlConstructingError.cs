namespace Softhouse.Converter.XmlConstructionModels;

public sealed class XmlConstructingError
{
    public string[] Messages { get; init; } = Array.Empty<string>();
    public XmlConstructingErrorStatus Status { get; set; }
}
