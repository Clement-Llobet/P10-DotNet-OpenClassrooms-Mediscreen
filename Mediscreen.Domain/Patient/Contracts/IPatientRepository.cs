using Mediscreen.Domain.Patient.Dto;

namespace Mediscreen.Domain.Patient.Contracts;

public interface IPatientRepository : IQueryable<IPatient>
{
    Task<int> UpdatePatient(PatientInput patientInput);
}
