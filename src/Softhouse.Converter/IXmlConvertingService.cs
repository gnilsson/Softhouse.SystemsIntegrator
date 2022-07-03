using Softhouse.Converter.XmlConstructionModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter;

public interface IXmlConvertingService
{
    XmlConstructionResult ConstructXmlDocument(RowInputFormat[] rowInputFormats);
}
