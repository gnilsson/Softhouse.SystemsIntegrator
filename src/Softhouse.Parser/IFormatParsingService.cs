using Softhouse.Shared.Metadata;

namespace Softhouse.Parser;

public interface IFormatParsingService
{
    IEnumerable<RowParsingResult> Parse(string input);
}
