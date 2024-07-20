using Mediscreen.Domain.Note.Contracts;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    private readonly INotesRepository _noteRepository;

    public NoteManager(INotesRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<List<NoteOutput>> GetNotesAsync()
    {
        return await _noteRepository.GetNotesAsync();
    }

    public async Task<NoteOutput> GetNoteAsync(int patientId)
    {
        return await _noteRepository.GetNoteAsync(patientId);
    }

    public async Task CreateNoteAsync(NoteCreateInput note)
    {
        await _noteRepository.CreateNoteAsync(note);
    }

    public async Task UpdateNoteAsync(int patientId, NoteUpdateInput noteIn)
    {
        await _noteRepository.UpdateNoteAsync(patientId, noteIn);
    }

    public async Task DeleteNoteAsync(int patientId)
    {
        await _noteRepository.DeleteNoteAsync(patientId);
    }
}
