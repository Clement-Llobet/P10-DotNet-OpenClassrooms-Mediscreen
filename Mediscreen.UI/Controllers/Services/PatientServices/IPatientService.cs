using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.PatientServices;

public interface IPatientService
{
    Task<IEnumerable<PatientViewModel>> GetAllPatients();
    Task<PatientViewModel> GetPatientById(int id);
    Task<PatientViewModel> UpdatePatient(int id, PatientViewModel patient);
    Task<PatientViewModel> CreatePatient(PatientViewModel patient);
}
