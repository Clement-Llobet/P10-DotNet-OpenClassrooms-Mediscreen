using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Repository;

public class NotesRepository : QueryableRepositoryBase<INotes>, INotesRepository
{
    private readonly IMongoCollection<Notes> _notes;

    public NotesRepository(IQueryable<INotes> notes, MongoClient client) : base(notes)
    {
        var database = client.GetDatabase("Mediscreen");
        _notes = database.GetCollection<Notes>("Notes");
    }

    public async Task<IEnumerable<INotes>> GetNotesAsync()
    {
        return await _notes.Find(note => true).ToListAsync();
    }

    public async Task<INotes> GetNoteAsync(int patientId)
    {
        return await _notes.Find(note => note.PatientId == patientId).FirstOrDefaultAsync();
    }

    public async Task CreateNoteAsync(NotesCreateInput noteInput, int practitionerId)
    {
        var newNote = new Notes
        {
            PatientId = noteInput.PatientId,
            Note = noteInput.Note ?? "",
            DoctorId = practitionerId,
            CreatedDate = noteInput.CreatedDate,
            LastUpdatdDate = noteInput.CreatedDate
        };
        await _notes.InsertOneAsync(newNote);
    }

    public async Task UpdateNoteAsync(NotesUpdateInput notesInput, int practitionerId)
    {
        var updatedNote = new Notes
        {
            PatientId = notesInput.PatientId,
            Note = notesInput.Note ?? "",
            DoctorId = practitionerId,
            LastUpdatdDate = notesInput.CurrentDateTime
        };

        await _notes.ReplaceOneAsync(note => note.PatientId == notesInput.PatientId, updatedNote);
    }

    public async Task DeleteNoteAsync(int patientId)
    {
        await _notes.DeleteOneAsync(note => note.PatientId == patientId);
    }
}
