using Softhouse.Converter.Descriptive;
using Softhouse.Converter.Extensions;
using Softhouse.Converter.XmlConstructionModels;
using Softhouse.Shared.Metadata;
using System.Text;
using System.Xml.Linq;

namespace Softhouse.Converter;

public sealed class XmlConvertingService : IXmlConvertingService
{
    private readonly IPersonBuilderService _personBuilderService;

    public XmlConvertingService(IPersonBuilderService personBuilderService)
    {
        _personBuilderService = personBuilderService;
    }

    public XmlConstructionResult Convert(RowInputFormat[] rowInputFormats)
    {
        var xdoc = ConstructXDocument(rowInputFormats);

        if (xdoc is null) return new XmlConstructionResult
        {
            Error = new XmlConstructingError { Messages = new[] { "empty." }, Status = XmlConstructingErrorStatus.Empty },
        };

        if (TryGetXmlText(xdoc, out var xmlText, out var errorMessage) is false)
        {
            return new XmlConstructionResult
            {
                Error = new XmlConstructingError { Messages = new[] { errorMessage! }, Status = XmlConstructingErrorStatus.Invalid },
            };
        }

        var xmlDocument = xdoc.ToXmlDocument();

        return new XmlConstructionResult
        {
            Document = xmlDocument,
            Text = xmlText,
        };
    }

    private XDocument? ConstructXDocument(RowInputFormat[] rowInputFormats)
    {
        var xdoc = new XDocument();

        var people = _personBuilderService.Build(rowInputFormats).ToArray();

        if (people is null or { Length: 0 }) return null;

        var peopleElement = new XElement(XElementName.People);

        foreach (var person in people)
        {
            var personElement = new XElement(XElementName.Person);

            personElement.AddIfNotNull(XElementName.FirstName, person.FirstName);
            personElement.AddIfNotNull(XElementName.LastName, person.LastName);

            personElement.AddContactXElements(person);

            if (person.FamilyMembers is not null and { Length: > 0 })
            {
                foreach (var familyMember in person.FamilyMembers)
                {
                    var familyElement = new XElement(XElementName.Family);

                    familyElement.AddIfNotNull(XElementName.Name, familyMember.Name);
                    familyElement.AddIfNotNull(XElementName.Born, familyMember.BirthdateYear);

                    familyElement.AddContactXElements(familyMember);

                    personElement.Add(familyElement);
                }
            }

            peopleElement.Add(personElement);
        }

        xdoc.Add(peopleElement);

        return xdoc;
    }

    private static bool TryGetXmlText(XDocument xdoc, out string? text, out string? errorMessage)
    {
        text = string.Empty;
        errorMessage = string.Empty;

        try
        {
            using var memory = new MemoryStream();
            xdoc.Save(memory);
            text = Encoding.UTF8.GetString(memory.ToArray());
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            return false;
        }

        return true;
    }
}
