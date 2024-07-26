using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    private readonly INotesRepository _noteRepository;

    public NoteManager(INotesRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<NotesOutput>> GetNotesAsync()
    {
        var notes = await _noteRepository.GetNotesAsync();

        return notes.Select(NotesOutput.Render);
    }

    public async Task<NotesOutput> GetNoteAsync(int patientId)
    {
        var notes = await _noteRepository.GetNoteAsync(patientId);

        return NotesOutput.Render(notes);
    }

    public async Task CreateNoteAsync(NotesCreateInput note, int practitionerId)
    {
        await _noteRepository.CreateNoteAsync(note, practitionerId);
    }

    public async Task UpdateNoteAsync(NotesUpdateInput noteInput, int practitionerId)
    {
        await _noteRepository.UpdateNoteAsync(noteInput, practitionerId);
    }

    public async Task DeleteNoteAsync(int patientId)
    {
        await _noteRepository.DeleteNoteAsync(patientId);
    }
}
