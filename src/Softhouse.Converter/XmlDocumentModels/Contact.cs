namespace Softhouse.Converter.XmlDocumentModels;

public abstract record class Contact
{
    public Phone? Phone { get; init; }
    public Address? Address { get; init; }
}
