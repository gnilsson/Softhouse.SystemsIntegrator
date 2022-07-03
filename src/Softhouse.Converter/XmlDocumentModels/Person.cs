namespace Softhouse.Converter.XmlDocumentModels;

public sealed record Person : Contact
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public FamilyMember[]? FamilyMembers { get; init; }
}
