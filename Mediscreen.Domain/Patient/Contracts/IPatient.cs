namespace Mediscreen.Domain.Patient.Contracts;

public interface IPatient
{
    int Id { get; }
    string FirstName { get; }
    string LastName { get; }
    DateTime BirthDate { get; }
    string Gender { get; }
    string HomeAddress { get; }
    string PhoneNumber { get; }
}
