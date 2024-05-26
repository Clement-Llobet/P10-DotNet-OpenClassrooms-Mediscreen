using Mediscreen.API.Routes;
using Mediscreen.Domain.Patient;
using Mediscreen.Domain.Patient.Contracts;

namespace Mediscreen.API.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatientsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Patient.ListPatients, (IPatientRepository patientRepository) => PatientManager.ListPatients(patientRepository)).WithTags(ApiRoutes.Patient.Tag);
    }
}
