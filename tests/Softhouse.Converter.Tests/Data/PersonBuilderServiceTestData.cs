using Softhouse.Converter.XmlDocumentModels;

namespace Softhouse.Converter.Tests.Data;

public static class PersonBuilderServiceTestData
{
    public static IEnumerable<object[]> Get_Build_RowInputFormats_ReturnsEnumerablePerson(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessCaseParam1,
                new Person[]
                {
                    new Person { FirstName = "Elof", LastName = "Sundin" }
                }.AsEnumerable(),
            },
        };

        return allData.Take(numTests);
    }
}
