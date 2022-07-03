using System.Xml;

namespace Softhouse.Converter.XmlConstructionModels;

public sealed class XmlConstructionResult
{
    public XmlDocument? Document { get; init; }
    public XmlConstructingError? Error { get; init; }
    public string? Text { get; init; }
}