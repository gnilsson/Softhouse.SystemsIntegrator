namespace Softhouse.Shared.Metadata;

public sealed class RowInputFormat
{
    public RowCategory Category { get; init; }
    public string[] ValueColumns { get; init; } = default!;
}
