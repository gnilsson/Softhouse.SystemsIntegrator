using Softhouse.Shared.Metadata;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Softhouse.Parser.Tests.FormatParsingService;

public class FormatParsingServiceTests
{
    [Theory]
    [MemberData(nameof(FormatParsingServiceTestData.Parse_InputString_ReturnsEnumerableRowParsingResult), 2, MemberType = typeof(FormatParsingServiceTestData))]
    public void Parse_InputString_ReturnsEnumerableRowParsingResult(string input, RowParsingResult[] expectedResult)
    {
        //Arrange
        var formatParsingService = new Parser.FormatParsingService();

        //Act
        var result = formatParsingService.Parse(input);

        //Assert
        expectedResult.Should().BeEquivalentTo(result, ExcludeProperties);
    }

    private static EquivalencyAssertionOptions<RowParsingResult> ExcludeProperties(EquivalencyAssertionOptions<RowParsingResult> options)
    {
        options.Excluding(t => t.Warning);
        options.Excluding(t => t.Error!.Message);
        return options;
    }
}
