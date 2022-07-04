namespace Softhouse.Converter.XmlDocumentModels;

public sealed record FamilyMember : Contact
{
    public string? Name { get; init; }
    public string? BirthdateYear { get; init; }
}
