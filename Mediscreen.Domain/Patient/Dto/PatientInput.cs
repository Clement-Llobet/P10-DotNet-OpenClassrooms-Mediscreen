namespace Mediscreen.Domain.Patient.Dto;

public record PatientInput
{
    public required int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? HomeAddress { get; set; }
    public string? PhoneNumber { get; set; }
}
