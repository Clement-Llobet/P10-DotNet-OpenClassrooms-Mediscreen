using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.PatientServices;

public interface IPatientService
{
    Task<IEnumerable<PatientViewModel>> GetAllPatients();
}
