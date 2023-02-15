using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.DbEntities;

abstract class MongoDBEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string Id { get; init; } = ObjectId.GenerateNewId().ToString();
    public DateTime Created { get; init; } = DateTime.UtcNow;
}
