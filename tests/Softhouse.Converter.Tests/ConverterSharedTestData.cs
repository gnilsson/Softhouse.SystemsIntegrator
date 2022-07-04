using Softhouse.Shared.Metadata;

namespace Softhouse.Converter.Tests;

public static class ConverterSharedTestData
{
    public static RowInputFormat[] RowInputFormatsSuccessSingle => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Elof", "Sundin" } },
    };

    public static RowInputFormat[] RowInputFormatsSuccessMultiple => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Elof", "Sundin" } },
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Boris", "Johnson" } },
    };

    public static RowInputFormat[] RowInputFormatsFailureEmpty1 => Array.Empty<RowInputFormat>();

    public static RowInputFormat[] RowInputFormatsFailureEmpty2 => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Telephone, ValueColumns = new[] { "073-101801", "018-101801" } },
        new RowInputFormat { Category = RowCategory.Address, ValueColumns = new[] { "S:t Johannesgatan 16", "Uppsala", "75330" } },
    };

    public static RowInputFormat[] RowInputFormatsFailureInvalid => new RowInputFormat[]
    {
        new RowInputFormat { Category = RowCategory.Person, ValueColumns = new[] { "Bob", "E\u001a" } },
    };

    public const string ExpectedXmlTextSuccessSingle = @"﻿<?xml version=""1.0"" encoding=""utf-8""?>
<people>
  <person>
    <firstname>Elof</firstname>
    <lastname>Sundin</lastname>
  </person>
</people>";

    public const string ExpectedXmlTextSuccessMultiple = @"﻿<?xml version=""1.0"" encoding=""utf-8""?>
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
