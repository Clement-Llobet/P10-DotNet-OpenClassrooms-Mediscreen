namespace Mediscreen.Domain.Patient.Contracts;

public interface IPatient
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime BirthDate { get; set; }
    string Gender { get; set; }
    string HomeAddress { get; set; }
    string PhoneNumber { get; set; }
}
