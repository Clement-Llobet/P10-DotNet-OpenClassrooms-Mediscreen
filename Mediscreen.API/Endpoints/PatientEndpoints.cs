using Mediscreen.API.Routes;
using Mediscreen.Domain.Patient;

namespace Mediscreen.API.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatients(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Patient.ListPatients, PatientManager.ListPatients).WithTags(ApiRoutes.Patient.Tag);
    }
}
