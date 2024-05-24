namespace Mediscreen.Infrastructure.SqlServerDatabase.Entities;

public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string HomeAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
