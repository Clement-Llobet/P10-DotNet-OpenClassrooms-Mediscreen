namespace Mediscreen.Domain.Patient.Dto;

public record PatientInputCreate
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string Gender { get; set; }
    public string? HomeAddress { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
}