using Mediscreen.Domain.Triggers.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

public class Triggers : ITriggers
{
    [BsonId]
    [BsonElement("triggerId")]
    public int TriggerId { get; set; }
    [BsonElement("triggerName")]
    public string TriggerName { get; set; } = string.Empty;
}
