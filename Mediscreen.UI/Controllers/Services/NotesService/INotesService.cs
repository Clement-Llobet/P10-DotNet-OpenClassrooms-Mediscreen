using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.NotesService;

public interface INotesService
{
    Task<IEnumerable<NotesViewModel>> GetAllPatientNotes();
    Task<NotesViewModel> GetPatientNoteById(int id);
    Task<NotesViewModel> UpdateNote(int id, NotesViewModel note);
    Task<NotesViewModel> CreateNote();
}
