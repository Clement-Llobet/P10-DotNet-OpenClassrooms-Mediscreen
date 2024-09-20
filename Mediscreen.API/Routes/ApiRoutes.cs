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
        public const string CreatePatient = Base;
        public static readonly SwaggerOperationAttribute CreatePatientMetadata = new("Create patient", "Create a new patient");
    }

    public static class Notes
    {
        private const string Base = "/api/notes";
        public const string Tag = "Notes";
        public const string ListNotes = Base + "/patient/{patientId}";
        public static readonly SwaggerOperationAttribute ListNotesMetadata = new("Get all notes", "Retrieve all patient notes from the MongoDB database");
        public const string GetNote = Base + "/{noteId}";
        public static readonly SwaggerOperationAttribute GetNoteMetadata = new("Get a single note", "Retrieve a patient note from the MongoDB database");
        public const string CreateNote = Base;
        public static readonly SwaggerOperationAttribute CreateNoteMetadata = new("Create a note", "Create a new note");
        public const string UpdateNote = Base + "/{noteId}";
        public static readonly SwaggerOperationAttribute UpdateNoteMetadata = new("Update a note", "Update a note");
    }

    public static class Triggers
    {
        private const string Base = "/api/triggers";
        public const string Tag = "Triggers";
        public const string ListTriggers = Base;
        public static readonly SwaggerOperationAttribute ListTriggersMetadata = new("Get all triggers", "Retrieve all triggers from the MongoDB database");
    }
}
