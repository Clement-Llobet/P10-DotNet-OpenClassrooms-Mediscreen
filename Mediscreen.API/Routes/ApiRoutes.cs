using Swashbuckle.AspNetCore.Annotations;

namespace Mediscreen.API.Routes;

public static class ApiRoutes
{
    public static class Patient
    {
        private const string Base = "/api/patients";
        public const string Tag = "Patients";
        public const string ListPatients = Base;
        public static readonly SwaggerOperationAttribute ListPatientsMetadata = new("Get all patients", "Retrieve all patients from the SQL Server database");
        public const string GetPatient = Base + "/{id}";
        public static readonly SwaggerOperationAttribute GetPatientMetadata = new("Get patient by id", "Retrieve a single patient from the SQL Server database");
        public const string UpdatePatient = Base + "/{id}";
        public static readonly SwaggerOperationAttribute UpdatePatientMetadata = new("Update patient by id", "Update a single patient");
    }
}
