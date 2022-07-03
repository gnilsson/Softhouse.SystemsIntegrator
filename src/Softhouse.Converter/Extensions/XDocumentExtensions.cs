using System.Xml;
using System.Xml.Linq;

namespace Softhouse.Converter.Extensions;

internal static class XDocumentExtensions
{
    internal static XmlDocument ToXmlDocument(this XDocument xdoc)
    {
        var xmlDocument = new XmlDocument();

        using var xmlReader = xdoc.CreateReader();

        xmlDocument.Load(xmlReader);

        return xmlDocument;
    }
}
