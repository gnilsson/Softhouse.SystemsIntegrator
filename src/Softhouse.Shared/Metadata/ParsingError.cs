namespace Softhouse.Shared.Metadata;

public sealed class RowParsingError
{
    public string Message { get; init; } = default!;
    public RowParsingErrorStatus Status { get; init; } = default!;
}
