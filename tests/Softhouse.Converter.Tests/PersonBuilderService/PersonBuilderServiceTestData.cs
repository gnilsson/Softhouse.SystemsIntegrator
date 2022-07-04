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
                ConverterSharedTestData.RowInputFormatsSuccessCase1,
                new Person[]
                {
                    new Person { FirstName = "Elof", LastName = "Sundin" }
                }.AsEnumerable(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessCase2,
                new Person[]
                {
                    new Person { FirstName = "Elof", LastName = "Sundin" },
                    new Person { FirstName = "Boris", LastName = "Johnson" }
                }.AsEnumerable(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsFailureCase1,
                Enumerable.Empty<Person>(),
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsFailureCase2,
                new Person[]
                {
                    new Person { FirstName = "Bob", LastName = "E\u001a" },
                }.AsEnumerable(),
            },
        };

        return allData.Take(numTests);
    }
}
