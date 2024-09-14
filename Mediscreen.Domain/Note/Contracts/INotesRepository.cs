using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.Domain.Note.Contracts;

public interface INotesRepository : IQueryable<INotes>
{
    Task CreateNoteAsync(NotesCreateInput notesInput);
    Task UpdateNoteAsync(NotesUpdateInput notesInput, int noteId);
    Task<(IPatient, IEnumerable<INotes>, IEnumerable<ITriggers>)> GetNotesAsync(int patientId);
    Task<(IPatient, INotes, IEnumerable<ITriggers>)> GetNoteAsync(int patientId);
}
