using Mediscreen.Domain.Common;
using Mediscreen.Domain.Patient.Contracts;

namespace Mediscreen.Domain.Patient.Dto;

public record PatientOutput
{
    public required int Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required int Age { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string Gender { get; set; } = string.Empty;
    public required string HomeAddress { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; } = string.Empty;

    public static PatientOutput Render(IPatient patient)
    {
        return new PatientOutput
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Age = AgeCalculation.CalculateAge(patient.BirthDate),
            BirthDate = patient.BirthDate,
            Gender = patient.Gender,
            HomeAddress = patient.HomeAddress,
            PhoneNumber = patient.PhoneNumber
        };
    }
}
