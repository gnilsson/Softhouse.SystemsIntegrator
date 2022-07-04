using Softhouse.Shared.Metadata;

namespace Softhouse.Parser.Tests;

public class FormatParsingServiceTestData
{
    public static IEnumerable<object[]> Parse_InputString_ReturnsEnumerableRowParsingResult(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[]
            {
                @"
P|Elof|Sundin
T|073-101801|018-101801
A|S:t Johannesgatan 16|Uppsala|75330
F|Hans|1967
A|Frodegatan 13B|Uppsala|75325
F|Anna|1969
T|073-101802|08-101802
P|Boris|Johnson
A|10 Downing Street|London
F|John|1970",
                new RowParsingResult[]
                {
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Person, ValueColumns = new[] { "Elof", "Sundin" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Telephone, ValueColumns = new[] { "073-101801", "018-101801" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Address, ValueColumns = new[] { "S:t Johannesgatan 16", "Uppsala", "75330" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Family, ValueColumns = new[] { "Hans", "1967" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Address, ValueColumns = new[] { "Frodegatan 13B", "Uppsala", "75325" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Family, ValueColumns = new[] { "Anna", "1969" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Telephone, ValueColumns = new[] { "073-101802", "08-101802" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Person, ValueColumns = new[] { "Boris", "Johnson" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Address, ValueColumns = new[] { "10 Downing Street", "London" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Family, ValueColumns = new[] { "John", "1970" } } }
                }
            },
            new object[]
            {
                @"
P|Elof
T||
A|S:t Johannesgatan 16|Uppsala|75330|5tr
X|
Y

P|Bob|E\u001a",
                new RowParsingResult[]
                {
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Person, ValueColumns = new[] { "Elof" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Telephone, ValueColumns = new[] { "", "" } } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Address, ValueColumns = new[] { "S:t Johannesgatan 16", "Uppsala", "75330", "5tr" } } },
                    new RowParsingResult { Error = new() { Status = RowParsingErrorStatus.Invalid } },
                    new RowParsingResult { Error = new() { Status = RowParsingErrorStatus.Empty } },
                    new RowParsingResult { RowInputFormat = new() { Category = RowCategory.Person, ValueColumns = new[] { "Bob", "E\\u001a" } } },
                }
            }
        };

        return allData.Take(numTests);
    }
}
