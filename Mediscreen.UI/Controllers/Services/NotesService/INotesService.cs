using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.NotesService;

public interface INotesService
{
    Task<IEnumerable<GetNotesViewModel>> GetAllPatientNotes(int patientId);
    Task<GetNotesViewModel> GetPatientNoteById(int noteId);
    Task<HttpResponseMessage> UpdateNote(NotesViewModel note);
    Task<HttpResponseMessage> CreateNote(NotesViewModel note);
}
