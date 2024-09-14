using Mediscreen.Domain.Common;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using MongoDB.Driver;
using static Azure.Core.HttpHeader;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Repository;

public class NotesRepository : QueryableRepositoryBase<INotes>, INotesRepository
{
    private readonly IMongoCollection<Notes> _notes;
    private readonly IMongoCollection<Triggers> _triggers;
    private readonly MediscreenSqlServerContext _dbContext;

    public NotesRepository(IQueryable<INotes> notes, IQueryable<ITriggers> triggers, MongoClient client, MediscreenSqlServerContext mediscreenSqlServerContext) : base(notes)
    {
        var database = client.GetDatabase("Mediscreen");
        _notes = database.GetCollection<Notes>("Notes");
        _triggers = database.GetCollection<Triggers>("Triggers");
        _dbContext = mediscreenSqlServerContext;
    }

    public async Task<(IPatient, IEnumerable<INotes>, IEnumerable<ITriggers>)> GetNotesAsync(int patientId)
    {
        var patient = await _dbContext.FindAsync<Patient>(patientId) ?? throw new Exception("Patient not found");
        var notes = await _notes.Find(note => note.PatientId == patientId).ToListAsync();
        var triggers = await _triggers.Find(trigger => notes.SelectMany(note => note.TriggersIds).Contains(trigger.TriggerId)).ToListAsync();

        return (patient, notes, triggers);
    }

    public async Task<(IPatient, INotes, IEnumerable<ITriggers>)> GetNoteAsync(int noteId)
    {
        var note = await _notes.Find(note => note.NoteId == noteId).FirstOrDefaultAsync();
        var patient = await _dbContext.FindAsync<Patient>(note.PatientId) ?? throw new Exception("Patient not found");
        var triggers = await _triggers.Find(trigger => note.TriggersIds.Contains(trigger.TriggerId)).ToListAsync();

        return (patient, note, triggers);
    }

    public async Task CreateNoteAsync(NotesCreateInput noteInput)
    {
        var patient = await _dbContext.FindAsync<Patient>(noteInput.PatientId);

        var triggers = await _triggers.Find(trigger => noteInput.Triggers.Contains(trigger.TriggerId)).ToListAsync();

        if (patient == null)
            throw new Exception("Patient not found");

        var newNote = new Notes
        {
            NoteId = Guid.NewGuid().GetHashCode(),
            PatientId = noteInput.PatientId,
            Comment = noteInput.Comment ?? "",
            DoctorId = noteInput.Practitioner!,
            CreatedDate = noteInput.CreatedDate,
            LastUpdatedDate = noteInput.CreatedDate,
            TriggersIds = triggers.Select(trigger => trigger.TriggerId).ToList()
        };

        await _notes.InsertOneAsync(newNote);
    }

    public async Task UpdateNoteAsync(NotesUpdateInput noteInput, int noteId)
    {
        var patient = await _dbContext.FindAsync<Patient>(noteInput.PatientId);

        var triggers = await _triggers.Find(trigger => noteInput.Triggers.Contains(trigger.TriggerId)).ToListAsync();

        if (patient == null)
            throw new Exception("Patient not found");

        var updatedNote = new Notes
        {
            NoteId = noteInput.NoteId,
            PatientId = noteInput.PatientId,
            Comment = noteInput.Comment ?? "",
            LastUpdatedDate = DateTime.Now,
            DoctorId = noteInput.Practitioner,
            TriggersIds = triggers.Select(trigger => trigger.TriggerId).ToList()

        };

        await _notes.ReplaceOneAsync(note => note.NoteId == noteInput.NoteId, updatedNote);
    }
}
