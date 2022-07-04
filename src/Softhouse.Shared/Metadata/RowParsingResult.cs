namespace Softhouse.Shared.Metadata;

public sealed class RowParsingResult
{
    public RowInputFormat? RowInputFormat { get; init; }
    public RowParsingError? Error { get; init; }
    public string? Warning { get; init; }
}
