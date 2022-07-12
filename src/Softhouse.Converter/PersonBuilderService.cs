using Softhouse.Converter.XmlDocumentModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter;

public sealed class PersonBuilderService : IPersonBuilderService
{
    private sealed class ContactRowInput
    {
        public RowInputFormat? Address { get; init; }
        public RowInputFormat? Phone { get; init; }
    }

    public IEnumerable<Person> Build(RowInputFormat[] rowInputFormats)
    {
        return YieldPerson(rowInputFormats);
    }

    private static IEnumerable<Person> YieldPerson(RowInputFormat[] rowInputFormats)
    {
        var personTargetRows = rowInputFormats
            .Where(x => x.Category is RowCategory.Person)
            .ToArray();

        for (int i = 0; i < personTargetRows.Length; i++)
        {
            var (personRows, personContactRowInput) = GetRelevantRowInputs(rowInputFormats, personTargetRows, i);

            var familyTargetRows = personRows
                .Where(x => x.Category is RowCategory.Family)
                .ToArray();

            var familyContactRowInputs = YieldFamilyContactRowInput(personRows, familyTargetRows).ToArray();

            var person = new Person
            {
                FirstName = PersonPropertyBuilder.Build(personTargetRows[i], 0),
                LastName = PersonPropertyBuilder.Build(personTargetRows[i], 1),
                Address = PersonPropertyBuilder.BuildRow(personContactRowInput.Address, (row) => new Address
                {
                    Street = PersonPropertyBuilder.BuildColumn(row, 0),
                    City = PersonPropertyBuilder.BuildColumn(row, 1),
                    Zip = PersonPropertyBuilder.BuildColumn(row, 2)
                }),
                Phone = PersonPropertyBuilder.BuildRow(personContactRowInput.Phone, (row) => new Phone
                {
                    Mobile = PersonPropertyBuilder.BuildColumn(row, 0),
                    Landline = PersonPropertyBuilder.BuildColumn(row, 1)
                }),
                FamilyMembers = PersonPropertyBuilder.BuildRows(familyTargetRows, (row, i) => new FamilyMember
                {
                    Name = PersonPropertyBuilder.BuildColumn(row, 0),
                    BirthdateYear = PersonPropertyBuilder.BuildColumn(row, 1),
                    Address = PersonPropertyBuilder.BuildRow(familyContactRowInputs[i].Address, (row) => new Address
                    {
                        Street = PersonPropertyBuilder.BuildColumn(row, 0),
                        City = PersonPropertyBuilder.BuildColumn(row, 1),
                        Zip = PersonPropertyBuilder.BuildColumn(row, 2)
                    }),
                    Phone = PersonPropertyBuilder.BuildRow(familyContactRowInputs[i].Phone, (row) => new Phone
                    {
                        Mobile = PersonPropertyBuilder.BuildColumn(row, 0),
                        Landline = PersonPropertyBuilder.BuildColumn(row, 1)
                    }),
                })?.ToArray()
            };

            yield return person;
        }
    }

    private static (RowInputFormat[], ContactRowInput) GetRelevantRowInputs(RowInputFormat[] baseRowInputs, RowInputFormat[] targetRowInputs, int iterator)
    {
        var lineNumber = Array.IndexOf(baseRowInputs, targetRowInputs[iterator]) + 1;

        var nextLineNumber = iterator == targetRowInputs.Length - 1
            ? baseRowInputs.Length
            : Array.IndexOf(baseRowInputs, targetRowInputs[iterator + 1]);

        var rows = baseRowInputs[lineNumber..nextLineNumber];

        var addressTargetRow = rows.FirstOrDefault(x => x.Category is RowCategory.Address);
        var phoneTargetRow = rows.FirstOrDefault(x => x.Category is RowCategory.Telephone);

        return (rows, new ContactRowInput
        {
            Address = addressTargetRow,
            Phone = phoneTargetRow,
        });
    }

    private static IEnumerable<ContactRowInput> YieldFamilyContactRowInput(RowInputFormat[] baseRowInputs, RowInputFormat[] targetRowInputs)
    {
        for (int i = 0; i < targetRowInputs.Length; i++)
        {
            var (_, contactRowInputs) = GetRelevantRowInputs(baseRowInputs, targetRowInputs, i);

            yield return contactRowInputs;
        }
    }
}
