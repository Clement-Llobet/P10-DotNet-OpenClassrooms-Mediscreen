using Mediscreen.API.Extensions;
using Mediscreen.API.Routes;
using Mediscreen.Domain.Note;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Mediscreen.API.Endpoints;

public static class NotesEndpoints
{
    public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Notes.ListNotes, async (INotesRepository noteRepository, IDistributedCache cache) =>
        {
            var cacheKey = "ListNotes";
            var notes = await CacheHelper.GetFromCacheAsync<IEnumerable<NotesOutput>>(cache, cacheKey);

            if (notes == null)
            {
                notes = await NoteManager.ListNotesAsync(noteRepository);
                await CacheHelper.SetInCacheAsync(cache, cacheKey, notes, CacheOptions.DefaultExpiration);
            }

            return notes;
        }).WithTags(ApiRoutes.Notes.Tag)
          .WithMetadata(ApiRoutes.Notes.ListNotesMetadata);

       app.MapGet(ApiRoutes.Notes.GetNote, async (INotesRepository noteRepository, IDistributedCache cache, int id) =>
       {
           var cacheKey = $"Note-{id}";
           var note = await CacheHelper.GetFromCacheAsync<NotesOutput>(cache, cacheKey);

           if (note == null)
           {
               note = NoteManager.GetNoteAsync(noteRepository, id).Result;
               await CacheHelper.SetInCacheAsync(cache, cacheKey, note, CacheOptions.DefaultExpiration);
           }

           return note;
       }).WithTags(ApiRoutes.Notes.Tag)
         .WithMetadata(ApiRoutes.Notes.GetNoteMetadata);
        
        app.MapPost(ApiRoutes.Notes.CreateNote, async (INotesRepository noteRepository, [Bind("PatientId,PractitionerId,Note")] NotesCreateInput noteCreateInput, int id) => await NoteManager.CreateNoteAsync(noteRepository, noteCreateInput, id))
            .WithTags(ApiRoutes.Notes.Tag)
            .WithMetadata(ApiRoutes.Notes.CreateNoteMetadata);

        app.MapPut(ApiRoutes.Notes.UpdateNote, async (INotesRepository noteRepository, [Bind("PatientId,PractitionerId,Note")] NotesUpdateInput noteUpdateInput, int id) => await NoteManager.UpdateNoteAsync(noteRepository, noteUpdateInput, id))
            .WithTags(ApiRoutes.Notes.Tag)
            .WithMetadata(ApiRoutes.Notes.UpdateNoteMetadata);

        app.MapDelete(ApiRoutes.Notes.DeleteNote, async (INotesRepository noteRepository, int id) => await NoteManager.DeleteNoteAsync(noteRepository, id))
            .WithTags(ApiRoutes.Notes.Tag)
            .WithMetadata(ApiRoutes.Notes.DeleteNoteMetadata);
    }
}
