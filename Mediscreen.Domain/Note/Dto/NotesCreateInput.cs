namespace Mediscreen.Domain.Note.Dto;

public record NotesCreateInput
{
    public required int PatientId { get; set; }
    public required int NoteId { get; set; }
    public required string Comment { get; set; } = string.Empty;
    public required List<string> Triggers { get; set; } = new();
    public required DateTime CreatedDate { get; set; }
    public string? Practitioner { get; set; } = string.Empty;
}
