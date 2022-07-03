namespace Softhouse.Converter.XmlDocumentModels;

public sealed record FamilyMember : Contact
{
    public string? FirstName { get; init; }
    public string? YearOfBirth { get; init; }
}
