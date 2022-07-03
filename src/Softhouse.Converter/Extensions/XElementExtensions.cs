using Softhouse.Converter.Descriptive;
using Softhouse.Converter.XmlDocumentModels;
using System.Xml.Linq;

namespace Softhouse.Converter.Extensions;

internal static class XElementExtensions
{
    internal static void AddIfNotNull(this XElement element, XName xname, string? content)
    {
        if (string.IsNullOrEmpty(content)) return;

        element.Add(new XElement(xname, content));
    }

    internal static void AddContactXElements<T>(this XElement baseElement, T contact) where T : Contact
    {
        if (contact.Address is not null)
        {
            var addressElement = new XElement(XElementName.Address);
            addressElement.AddIfNotNull(XElementName.Street, contact.Address?.Street);
            addressElement.AddIfNotNull(XElementName.City, contact.Address?.City);
            addressElement.AddIfNotNull(XElementName.Zip, contact.Address?.Zip);
            baseElement.Add(addressElement);
        }

        if (contact.Phone is not null)
        {
            var phoneElement = new XElement(XElementName.Phone);
            phoneElement.AddIfNotNull(XElementName.Mobile, contact.Phone?.Mobile);
            phoneElement.AddIfNotNull(XElementName.Landline, contact.Phone?.Landline);
            baseElement.Add(phoneElement);
        }
    }
}
