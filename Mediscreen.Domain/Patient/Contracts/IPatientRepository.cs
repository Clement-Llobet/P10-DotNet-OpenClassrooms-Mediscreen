using Mediscreen.Domain.Patient.Dto;

namespace Mediscreen.Domain.Patient.Contracts;

public interface IPatientRepository : IQueryable<IPatient>
{
    int UpdatePatient(PatientInput patientInput);
}
