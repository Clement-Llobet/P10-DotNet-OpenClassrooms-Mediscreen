﻿using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note.Contracts;

public interface INotesRepository : IQueryable<INotes>
{
    Task CreateNoteAsync(NotesCreateInput notesInput, int practitionerId);
    Task UpdateNoteAsync(NotesUpdateInput notesInput, int noteId, int practitionerId);
    Task<IEnumerable<INotes>> GetNotesAsync(int patientId);
    Task<INotes> GetNoteAsync(int patientId);
}
