using Softhouse.Shared.Metadata;

namespace Softhouse.Converter.Tests;

public static class ConverterSharedTestData
{
    public static RowInputFormat[] RowInputFormatsSuccessCase1 => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Elof", "Sundin" } },
    };

    public static RowInputFormat[] RowInputFormatsSuccessCase2 => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Elof", "Sundin" } },
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Boris", "Johnson" } },
    };

    public static RowInputFormat[] RowInputFormatsFailureCase1 => Array.Empty<RowInputFormat>();

    public static RowInputFormat[] RowInputFormatsFailureCase2 => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Bob", "E\u001a" } },
    };

    public const string ExpectedXmlTextSuccessCaseParam1 = @"﻿<?xml version=""1.0"" encoding=""utf-8""?>
<people>
  <person>
    <firstname>Elof</firstname>
    <lastname>Sundin</lastname>
  </person>
</people>";

    public const string ExpectedXmlTextSuccessCaseParam2 = @"﻿<?xml version=""1.0"" encoding=""utf-8""?>
<people>
  <person>
    <firstname>Elof</firstname>
    <lastname>Sundin</lastname>
  </person>
  <person>
    <firstname>Boris</firstname>
    <lastname>Johnson</lastname>
  </person>
</people>";
}
