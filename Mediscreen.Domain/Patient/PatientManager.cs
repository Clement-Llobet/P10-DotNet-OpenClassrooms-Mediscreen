using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;

namespace Mediscreen.Domain.Patient;

public static class PatientManager
{
    public static List<PatientOutput> ListPatients(IPatientRepository patientRepository)
    {
        var request = patientRepository.ToList();

        return request.Select(PatientOutput.Render).ToList();
    }
}
