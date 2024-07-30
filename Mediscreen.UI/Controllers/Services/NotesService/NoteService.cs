using Mediscreen.Domain.Note.Dto;
using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.NotesService;

public class NoteService : INotesService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public NoteService(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
    {
        _client = httpClientFactory.CreateClient("DisableSslValidationHttpClient");
        _baseUrl = GetBaseUrl(env);
    }

    private static string GetBaseUrl(IWebHostEnvironment env)
    {
        return env.IsDevelopment() ? "http://host.docker.internal:65255" : $"https://host.docker.internal:65256";
    }

    public Task<NotesViewModel> CreateNote(NotesViewModel note)
    {
        var noteInputCreate = new NotesCreateInput
        {
            PatientId = note.PatientId,
            NoteId = note.NoteId,
            Note = note.Note,
            CreatedDate = DateTime.Now,
            Practitioner = note.Practitioner
        };

        var response = _client.PostAsJsonAsync($"{_baseUrl}/api/notes", noteInputCreate);
        response.Result.EnsureSuccessStatusCode();

        return Task.FromResult(note);
    }

    public async Task<IEnumerable<NotesViewModel>> GetAllPatientNotes()
    {
        var response = await _client.GetAsync($"{_baseUrl}/api/notes");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<NotesViewModel>>();
        return content ?? new List<NotesViewModel>();
    }

    public async Task<NotesViewModel> GetPatientNoteById(int id) 
    {         
        var response = await _client.GetAsync($"{_baseUrl}/api/notes/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<NotesViewModel>();

        return content ?? throw new InvalidOperationException("Note not found");
    }

    public async Task<NotesViewModel> UpdateNote(int id, NotesViewModel note)
    {
        var noteInputUpdate = new NotesUpdateInput
        {
            NoteId = note.NoteId,
            PatientId = note.PatientId,
            Note = note.Note,
            CurrentDateTime = note.CreatedDate,
            Practitioner = note.Practitioner
        };

        var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/notes/{id}", noteInputUpdate);
        response.EnsureSuccessStatusCode();

        return note;
    }
}
