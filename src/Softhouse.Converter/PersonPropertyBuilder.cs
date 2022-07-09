using Softhouse.Shared.Metadata;

namespace Softhouse.Converter;

internal static class PersonPropertyBuilder
{
    internal static string? Build(RowInputFormat? row, int columnIndex)
    {
        return row is not null
            ? row.ValueColumns.Length > columnIndex
            ? row.ValueColumns[columnIndex]
            : null
            : null;
    }

    internal static string? BuildColumn(RowInputFormat row, int columnIndex)
    {
        return row.ValueColumns.Length > columnIndex
            ? row.ValueColumns[columnIndex]
            : null;
    }

    internal static T? BuildRow<T>(RowInputFormat? row, Func<RowInputFormat, T> builder)
    {
        if (row is null) return default;

        var result = builder(row);

        return result;
    }

    internal static IEnumerable<T>? BuildRows<T>(RowInputFormat[]? rows, Func<RowInputFormat, int, T> builder)
    {
        if (rows is null or { Length: 0 }) return null;

        return YieldRow(rows, builder);
    }

    private static IEnumerable<T> YieldRow<T>(RowInputFormat[] rows, Func<RowInputFormat, int, T> builder)
    {
        for (int i = 0; i < rows.Length; i++)
        {
            var row = rows[i];

            if (row is null) continue;

            var result = builder(row, i);

            yield return result;
        }
    }
}
