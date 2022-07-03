﻿using Softhouse.Converter.XmlConstructionModels;
using Softhouse.Converter.XmlDocumentModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter.Tests.Data;

public static class XmlConvertingServiceTestData
{
    public static IEnumerable<object[]> Get_ConstructXmlDocument_RowInputFormats_ReturnsXmlText(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessCaseParam1,
                (RowInputFormat[] input) => new Person[]
                {
                    new Person { FirstName = input[0].ValueColumns[0], LastName = input[0].ValueColumns[1] }
                },
                ConverterSharedTestData.ExpectedXmlTextSuccessCaseParam1,
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsSuccessCaseParam2,
                (RowInputFormat[] input) => new Person[]
                {
                    new Person { FirstName = input[0].ValueColumns[0], LastName = input[0].ValueColumns[1] },
                    new Person { FirstName = input[1].ValueColumns[0], LastName = input[1].ValueColumns[1] }
                },
                ConverterSharedTestData.ExpectedXmlTextSuccessCaseParam2,
            },
        };

        return allData.Take(numTests);
    }

    public static IEnumerable<object[]> Get_ConstructXmlDocument_ErrorRowInputFormats_ReturnsErrorStatus(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsFailureCaseParam1,
                (RowInputFormat[] input) => Array.Empty<Person>(),
                XmlConstructingErrorStatus.Empty,
            },
            new object[]
            {
                ConverterSharedTestData.RowInputFormatsFailureCaseParam2,
                (RowInputFormat[] input) => new Person[]
                {
                    new Person { FirstName = input[0].ValueColumns[0], LastName = input[0].ValueColumns[1] }
                },
                XmlConstructingErrorStatus.Invalid,
            },
        };

        return allData.Take(numTests);
    }
}
