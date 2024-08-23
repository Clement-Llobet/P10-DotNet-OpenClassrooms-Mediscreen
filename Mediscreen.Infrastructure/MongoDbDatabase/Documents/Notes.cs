using Mediscreen.Domain.Note.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

public class Notes : INotes
{
    [BsonId]
    [BsonElement("noteId")]
    public int NoteId { get; set; }
    [BsonElement("patientId")]
    public int PatientId { get; set; }
    [BsonElement("note")]
    public string Comment { get; set; } = string.Empty;
    [BsonElement("triggersList")]
    public List<Triggers> Triggers { get; set; } = new();
    [BsonElement("doctorId")]
    public string DoctorId { get; set; } = string.Empty;
    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; }
    [BsonElement("updatedDate")]
    public DateTime LastUpdatedDate { get; set; }
}
