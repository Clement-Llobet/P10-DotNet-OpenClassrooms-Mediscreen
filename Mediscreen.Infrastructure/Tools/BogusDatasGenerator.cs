using Bogus;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;

namespace Mediscreen.Infrastructure.Tools;

public class BogusDatasGenerator
{
    public IReadOnlyCollection<Patient> Patients { get; set; } = [];

    public BogusDatasGenerator()
    {
        Patients = GeneratePatients(amount: 5);
        
    }

    private static IReadOnlyCollection<Patient> GeneratePatients(int amount)
    {
        var patientFaker = new Faker<Patient>()
            .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.BirthDate, f => f.Date.Past(18))
            .RuleFor(p => p.Gender, f => f.PickRandomParam("M", "F"))
            .RuleFor(p => p.HomeAddress, f => f.Address.FullAddress())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber());

        var patients = Enumerable.Range(1, amount)
            .Select(i => SeedRow(patientFaker, i))
            .ToList();

        return patients;
    }

    private static T SeedRow<T>(Faker<T> faker, int rowId) where T : class
    {
        var recordRow = faker.UseSeed(rowId).Generate();
        return recordRow;
    }
}
