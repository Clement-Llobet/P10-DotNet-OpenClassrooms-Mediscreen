using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    public static async Task<IEnumerable<NotesOutput>> ListNotesFromPatientAsync(INotesRepository noteRepository, int patientId)
    {
        var notes = await noteRepository.GetNotesAsync(patientId);

        return notes.Select(NotesOutput.Render);
    }

    public static async Task<NotesOutput> GetNoteAsync(INotesRepository noteRepository, int noteId)
    {
        var notes = await noteRepository.GetNoteAsync(noteId);

        return NotesOutput.Render(notes);
    }

    public static async Task CreateNoteAsync(INotesRepository noteRepository, ITriggersRepository triggersRepository, NotesCreateInput note)
    {
        note.Triggers.Select(trigger => triggersRepository.GetTriggerAsync(trigger.TriggerId).Result).ToList();
        await noteRepository.CreateNoteAsync(note);
    }

    public static async Task UpdateNoteAsync(INotesRepository noteRepository, ITriggersRepository triggersRepository, NotesUpdateInput noteInput, int noteId)
    {
        noteInput.Triggers.Select(trigger => triggersRepository.GetTriggerAsync(trigger.TriggerId).Result);
        await noteRepository.UpdateNoteAsync(noteInput, noteId);
    }
}
