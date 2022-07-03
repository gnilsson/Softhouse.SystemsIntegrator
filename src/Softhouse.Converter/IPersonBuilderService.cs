using Softhouse.Converter.XmlDocumentModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter;

public interface IPersonBuilderService
{
    IEnumerable<Person> Build(RowInputFormat[] rowInputFormats);
}
