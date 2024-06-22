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

    public static PatientOutput GetPatient(IPatientRepository patientRepository, int id)
    {
        var request = patientRepository.FirstOrDefault(x => x.Id == id);

        return request == null ? throw new Exception("Patient not found") : PatientOutput.Render(request);
    }

    public static int UpdatePatient(IPatientRepository patientRepository, PatientInput patientInput)
    {
        return patientRepository.UpdatePatient(patientInput);
    }
}
