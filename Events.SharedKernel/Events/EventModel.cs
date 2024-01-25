using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Events.SharedKernel.Events;

public class EventModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public BaseEvent EventData { get; set; }
    
    public string EventType { get; set; }
    
    //sets datetime event occurs
    public DateTime TimeStamp { get; set; }
    
    public Guid AggregateIdentifier { get; set; }
    
    public string AggregateType { get; set; }
    
    public int Version { get; set; }
}