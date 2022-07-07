using Softhouse.Converter.XmlDocumentModels;

namespace Softhouse.Converter.Tests.PersonBuilderService;

public static class PersonBuilderServiceTestData
{
    public static IEnumerable<object[]> Get_Build_RowInputFormats_ReturnsEnumerablePerson(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessSingle,
                new Person[]
                {
                    new Person { FirstName = "Elof", LastName = "Sundin" }
                }.AsEnumerable(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessMultiple,
                new Person[]
                {
                    new Person { FirstName = "Elof", LastName = "Sundin" },
                    new Person { FirstName = "Boris", LastName = "Johnson" }
                }.AsEnumerable(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsFailureEmpty1,
                Enumerable.Empty<Person>(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsXmlFailureInvalid,
                new Person[]
                {
                    new Person { FirstName = "Bob", LastName = "" },
                }.AsEnumerable(),
            },
        };

        return allData.Take(numTests);
    }
}
