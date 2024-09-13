using Mediscreen.Domain.Common;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using MongoDB.Driver;

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

    public async Task<IEnumerable<INotes>> GetNotesAsync(int patientId)
    {
        return await _notes.Find(note => note.PatientId == patientId).ToListAsync();
    }

    public async Task<INotes> GetNoteAsync(int noteId)
    {
        return await _notes.Find(note => note.NoteId == noteId).FirstOrDefaultAsync();
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
            Triggers = triggers.Cast<ITriggers>().ToList(),
            RiskLevel = DiabetesRiskCalculator.CalculateRiskLevel(patient, noteInput.Triggers.Count).ToString()
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
            Triggers = triggers.Cast<ITriggers>().ToList(),
            RiskLevel = DiabetesRiskCalculator.CalculateRiskLevel(patient, noteInput.Triggers.Count).ToString()
        };

        await _notes.ReplaceOneAsync(note => note.NoteId == noteInput.NoteId, updatedNote);
    }
}
