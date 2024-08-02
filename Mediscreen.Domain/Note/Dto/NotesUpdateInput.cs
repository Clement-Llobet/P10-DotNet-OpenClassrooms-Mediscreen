namespace Mediscreen.Domain.Note.Dto;

public record NotesUpdateInput
{
    public int NoteId { get; set; }
    public required int PatientId { get; set; }
    public string? Note { get; set; }
    public required DateTime CurrentDateTime { get; set; }
    public required string Practitioner { get; set; } 
}
