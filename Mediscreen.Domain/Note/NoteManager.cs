﻿using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;

namespace Mediscreen.Domain.Note;

public class NoteManager
{
    public static async Task<IEnumerable<NotesOutput>> ListNotesFromPatientAsync(INotesRepository noteRepository, int patientId)
    {
        var notes = await noteRepository.GetNotesAsync(patientId);

        var returnValue = notes.Select(NotesOutput.Render);

        return returnValue;
    }

    public static async Task<NotesOutput> GetNoteAsync(INotesRepository noteRepository, int noteId)
    {
        var notes = await noteRepository.GetNoteAsync(noteId);

        return NotesOutput.Render(notes);
    }

    public static async Task CreateNoteAsync(INotesRepository noteRepository, NotesCreateInput note)
    {
        await noteRepository.CreateNoteAsync(note);
    }

    public static async Task UpdateNoteAsync(INotesRepository noteRepository, NotesUpdateInput noteInput, int noteId)
    {
        await noteRepository.UpdateNoteAsync(noteInput, noteId);
    }
}
