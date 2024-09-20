using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    public static async Task<List<NotesOutput>> ListNotesFromPatientAsync(INotesRepository noteRepository, int patientId)
    {
        return await noteRepository.GetAllNotesAsync(patientId);

        return notes.Select(NotesOutput.Render);
    }

    public static async Task<NotesOutput> GetNoteAsync(INotesRepository noteRepository, int noteId)
    {
        var notesRepositoryDatas = await noteRepository.GetNoteAsync(noteId);
        var patient = notesRepositoryDatas.Item1;
        var note = notesRepositoryDatas.Item2;
        var triggers = notesRepositoryDatas.Item3.ToList();

        return NotesOutput.Render(patient!, note, triggers);
    }

    public static async Task CreateNoteAsync(INotesRepository noteRepository, ITriggersRepository triggersRepository, NotesCreateInput note)
    {
        await noteRepository.CreateNoteAsync(note);
    }

    public static async Task UpdateNoteAsync(INotesRepository noteRepository, ITriggersRepository triggersRepository, NotesUpdateInput noteInput, int noteId)
    {
        noteInput.Triggers.Select(trigger => triggersRepository.GetTriggerAsync(trigger).Result);
        await noteRepository.UpdateNoteAsync(noteInput, noteId);
    }
}
