using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GymProgress.Api
{
    public class Seance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Exercice> Exercices { get; set; }
    }
}
