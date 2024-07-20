using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note.Contracts;

public interface INotesRepository : IQueryable<INotes>
{
    Task CreateNoteAsync(NotesCreateInput notesInput, int practitionerId);
    Task UpdateNoteAsync(int patientId, NotesUpdateInput notesInput, int practitionerId);
    Task DeleteNoteAsync(int patientId);
}
