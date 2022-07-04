using Softhouse.Shared.Metadata;

namespace Softhouse.Parser;

public sealed class FormatParsingService : IFormatParsingService
{
    private static readonly Dictionary<string, RowCategory> _rowCategories = new()
    {
        ["P"] = RowCategory.Person,
        ["F"] = RowCategory.Family,
        ["T"] = RowCategory.Telephone,
        ["A"] = RowCategory.Address,
    };

    private static readonly Dictionary<RowCategory, int> _maxValueColumnCountRuleSet = new()
    {
        [RowCategory.Person] = 2,
        [RowCategory.Family] = 2,
        [RowCategory.Address] = 3,
        [RowCategory.Telephone] = 2,
    };

    public IEnumerable<RowParsingResult> Parse(string input)
    {
        return YieldRowParsingResult(input);
    }

    private static IEnumerable<RowParsingResult> YieldRowParsingResult(string input)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var row in rows)
        {
            var columns = row.Split('|');

            if (columns.Length <= 1)
            {
                yield return new RowParsingResult
                {
                    Error = new RowParsingError { Message = "Row contains no value columns.", Status = RowParsingErrorStatus.Empty },
                };
                continue;
            }

            var categoryInput = columns[0].ToString();

            if (_rowCategories.TryGetValue(categoryInput, out var category) is false)
            {
                yield return new RowParsingResult
                {
                    Error = new RowParsingError { Message = $"Category '{categoryInput}' is not valid.", Status = RowParsingErrorStatus.Invalid }
                };
                continue;
            }

            var valueColumn = columns.TakeLast(columns.Length - 1).ToArray();

            var warning = valueColumn.Length > _maxValueColumnCountRuleSet[category]
                ? $"Row contains excessive value columns."
                : null;

            yield return new RowParsingResult
            {
                RowInputFormat = new RowInputFormat
                {
                    Category = category,
                    ValueColumns = valueColumn,
                },
                Warning = warning,
            };
        }
    }
}
