using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.NotesService;

public interface INotesService
{
    Task<IEnumerable<NotesViewModel>> GetAllPatientNotes(int patientId);
    Task<NotesViewModel> GetPatientNoteById(int noteId);
    Task<NotesViewModel> UpdateNote(int noteId, NotesViewModel note);
    Task<HttpResponseMessage> CreateNote(NotesViewModel note);
}
