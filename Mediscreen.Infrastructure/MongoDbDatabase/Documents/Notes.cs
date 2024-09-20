using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Triggers.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

[BsonIgnoreExtraElements]
public class Notes : INotes
{
    [BsonId]
    [BsonElement("noteId")]
    public int NoteId { get; set; }
    [BsonElement("patientId")]
    public int PatientId { get; set; }
    [BsonElement("comment")]
    public string Comment { get; set; } = string.Empty;
    [BsonElement("triggersList")]
    public List<int> TriggersIds { get; set; } = new List<int>();
    [BsonElement("doctorId")]
    public string DoctorId { get; set; } = string.Empty;
    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; }
    [BsonElement("updatedDate")]
    public DateTime LastUpdatedDate { get; set; }
}
