using Mediscreen.API.Extensions;
using Mediscreen.API.Routes;
using Mediscreen.Domain.Note;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Triggers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Mediscreen.API.Endpoints;

public static class NotesEndpoints
{
    public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Notes.ListNotes, async (INotesRepository noteRepository, IDistributedCache cache, int patientId) =>
        {
            var cacheKey = $"ListNotes-{patientId}";
            var notes = await CacheHelper.GetFromCacheAsync<IEnumerable<NotesOutput>>(cache, cacheKey);

            if (notes == null)
            {
                notes = await NoteManager.ListNotesFromPatientAsync(noteRepository, patientId);
                await CacheHelper.SetInCacheAsync(cache, cacheKey, notes, CacheOptions.DefaultExpiration);
            }

            return notes;
        }).WithTags(ApiRoutes.Notes.Tag)
          .WithMetadata(ApiRoutes.Notes.ListNotesMetadata);

       app.MapGet(ApiRoutes.Notes.GetNote, async (INotesRepository noteRepository, IDistributedCache cache, int noteId) =>
       {
           var cacheKey = $"Note-{noteId}";
           var note = await CacheHelper.GetFromCacheAsync<NotesOutput>(cache, cacheKey);

           if (note == null)
           {
               note = NoteManager.GetNoteAsync(noteRepository, noteId).Result;
               await CacheHelper.SetInCacheAsync(cache, cacheKey, note, CacheOptions.DefaultExpiration);
           }

           return note;
       }).WithTags(ApiRoutes.Notes.Tag)
         .WithMetadata(ApiRoutes.Notes.GetNoteMetadata);
        
        app.MapPost(ApiRoutes.Notes.CreateNote, async (INotesRepository noteRepository, ITriggersRepository triggersRepository, [Bind("PatientId,PractitionerId,Note")] NotesCreateInput noteCreateInput) => await NoteManager.CreateNoteAsync(noteRepository, triggersRepository, noteCreateInput))
            .WithTags(ApiRoutes.Notes.Tag)
            .WithMetadata(ApiRoutes.Notes.CreateNoteMetadata);

        app.MapPut(ApiRoutes.Notes.UpdateNote, async (INotesRepository noteRepository, ITriggersRepository triggersRepository, [Bind("NoteId,PatientId,PractitionerId,Note")] NotesUpdateInput noteUpdateInput, int noteId) => await NoteManager.UpdateNoteAsync(noteRepository, triggersRepository, noteUpdateInput, noteId))
            .WithTags(ApiRoutes.Notes.Tag)
            .WithMetadata(ApiRoutes.Notes.UpdateNoteMetadata);
    }
}
