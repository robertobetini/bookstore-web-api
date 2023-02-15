using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.DbEntities;

abstract class MongoDBEntity
{
    [BsonId]
    public virtual string Id { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
