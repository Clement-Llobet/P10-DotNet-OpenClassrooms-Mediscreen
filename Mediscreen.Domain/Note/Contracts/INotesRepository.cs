using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note.Contracts;

public interface INotesRepository : IQueryable<INotes>
{
    Task CreateNoteAsync(NotesCreateInput notesInput);
    Task UpdateNoteAsync(NotesUpdateInput notesInput, int noteId);
    Task<IEnumerable<INotes>> GetNotesAsync(int patientId);
    Task<INotes> GetNoteAsync(int patientId);
}
