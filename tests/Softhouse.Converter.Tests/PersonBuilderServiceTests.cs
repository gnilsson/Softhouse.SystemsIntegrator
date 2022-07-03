using Softhouse.Converter.Tests.Data;
using Softhouse.Converter.XmlDocumentModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter.Tests;

public class PersonBuilderServiceTests
{
    [Theory]
    [MemberData(nameof(PersonBuilderServiceTestData.Get_Build_RowInputFormats_ReturnsEnumerablePerson), 1, MemberType = typeof(PersonBuilderServiceTestData))]
    public void Build_RowInputFormats_ReturnsEnumerablePerson(RowInputFormat[] input, IEnumerable<Person>? expectedResult)
    {
        //Arrange
        var personBuilderService = new PersonBuilderService();

        //Act
        var result = personBuilderService.Build(input);

        //Assert
        Assert.Equal(expectedResult, result);
    }
}
