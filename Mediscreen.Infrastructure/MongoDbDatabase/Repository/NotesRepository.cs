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

    public async Task<IEnumerable<INotes>> GetNotesAsync(int patientId)
    {
        return await _notes.Find(note => note.PatientId == patientId).ToListAsync();
    }

    public async Task<INotes> GetNoteAsync(int noteId)
    {
        return await _notes.Find(note => note.NoteId == noteId).FirstOrDefaultAsync();
    }

    public async Task CreateNoteAsync(NotesCreateInput noteInput, int practitionerId)
    {
        var newNote = new Notes
        {
            NoteId = Guid.NewGuid().GetHashCode(),
            PatientId = noteInput.PatientId,
            Note = noteInput.Note ?? "",
            DoctorId = practitionerId,
            CreatedDate = noteInput.CreatedDate,
            LastUpdatdDate = noteInput.CreatedDate
        };
        await _notes.InsertOneAsync(newNote);
    }

    public async Task UpdateNoteAsync(NotesUpdateInput notesInput, int noteId)
    {
        var updatedNote = new Notes
        {
            NoteId = noteId,
            PatientId = notesInput.PatientId,
            Note = notesInput.Note ?? "",
            DoctorId = Convert.ToInt32(notesInput.Practitioner),
            LastUpdatdDate = notesInput.CurrentDateTime
        };

        await _notes.ReplaceOneAsync(note => note.NoteId == notesInput.NoteId, updatedNote);
    }
}
