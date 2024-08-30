﻿using Mediscreen.Domain.Common;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Repository;

public class NotesRepository : QueryableRepositoryBase<INotes>, INotesRepository
{
    private readonly IMongoCollection<Notes> _notes;
    private readonly MediscreenSqlServerContext _dbContext;

    public NotesRepository(IQueryable<INotes> notes, MongoClient client, MediscreenSqlServerContext mediscreenSqlServerContext) : base(notes)
    {
        var database = client.GetDatabase("Mediscreen");
        _notes = database.GetCollection<Notes>("Notes");
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

        if (patient == null)
            throw new Exception("Patient not found");

        var triggersList = noteInput.Triggers.Select(trigger => Enum.Parse<Triggers>(trigger.Trim())).ToList();

        var newNote = new Notes
        {
            NoteId = Guid.NewGuid().GetHashCode(),
            PatientId = noteInput.PatientId,
            Comment = noteInput.Comment ?? "",
            DoctorId = noteInput.Practitioner!,
            CreatedDate = noteInput.CreatedDate,
            LastUpdatedDate = noteInput.CreatedDate,
            Triggers = triggersList,
            RiskLevel = DiabetesRiskCalculator.CalculateRiskLevel(patient, triggersList).ToString()
        };
        await _notes.InsertOneAsync(newNote);
    }

    public async Task UpdateNoteAsync(NotesUpdateInput noteInput, int noteId)
    {
        var patient = await _dbContext.FindAsync<Patient>(noteInput.PatientId);

        if (patient == null)
            throw new Exception("Patient not found");

        var triggersList = noteInput.Triggers.Select(trigger => Enum.Parse<Triggers>(trigger.Trim())).ToList();

        var updatedNote = new Notes
        {
            NoteId = noteInput.NoteId,
            PatientId = noteInput.PatientId,
            Comment = noteInput.Comment ?? "",
            LastUpdatedDate = DateTime.Now,
            DoctorId = noteInput.Practitioner,
            Triggers = triggersList,
            RiskLevel = DiabetesRiskCalculator.CalculateRiskLevel(patient, triggersList).ToString()
        };

        await _notes.ReplaceOneAsync(note => note.NoteId == noteInput.NoteId, updatedNote);
    }
}
