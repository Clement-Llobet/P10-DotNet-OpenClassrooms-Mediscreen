namespace Mediscreen.Domain.Note.Contracts;

public interface INotes
{
    public int NoteId { get; set; }
    public int PatientId { get; set; }
    public string Comment { get; set; }
    public string DoctorId { get; set; }
    public DateTime CreatedDate { get; set; }
}
