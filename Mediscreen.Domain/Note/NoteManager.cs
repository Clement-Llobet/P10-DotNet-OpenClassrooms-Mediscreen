using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    public static async Task<IEnumerable<NotesOutput>> ListNotesAsync(INotesRepository noteRepository)
    {
        var notes = await noteRepository.GetNotesAsync();

        return notes.Select(NotesOutput.Render);
    }

    public static async Task<NotesOutput> GetNoteAsync(INotesRepository noteRepository, int patientId)
    {
        var notes = await noteRepository.GetNoteAsync(patientId);

        return NotesOutput.Render(notes);
    }

    public static async Task CreateNoteAsync(INotesRepository noteRepository, NotesCreateInput note, int practitionerId)
    {
        await noteRepository.CreateNoteAsync(note, practitionerId);
    }

    public static async Task UpdateNoteAsync(INotesRepository noteRepository, NotesUpdateInput noteInput, int practitionerId)
    {
        await noteRepository.UpdateNoteAsync(noteInput, practitionerId);
    }

    public static async Task DeleteNoteAsync(INotesRepository noteRepository, int patientId)
    {
        await noteRepository.DeleteNoteAsync(patientId);
    }
}
