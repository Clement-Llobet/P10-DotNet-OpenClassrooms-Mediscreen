namespace Mediscreen.UI.Models;

public class PatientViewModel
{
    public required int Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required DateTime BirthDate { get; set; }
    public required string Gender { get; set; } = string.Empty;
    public string HomeAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
