using Bogus;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;

namespace Mediscreen.Infrastructure.Tools;

public class BogusDatasGenerator
{
    Faker<Patient> patientFaker;

    public BogusDatasGenerator()
    {
        Randomizer.Seed = new Random(123);

        patientFaker = new Faker<Patient>()
            .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.BirthDate, f => f.Date.Past(18))
            .RuleFor(p => p.Gender, f => f.PickRandomParam("M", "F"))
            .RuleFor(p => p.HomeAddress, f => f.Address.FullAddress())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber());
    }

    public Patient GeneratePatient()
    {
        return patientFaker.Generate();
    }


}
