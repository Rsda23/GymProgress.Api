using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace GymProgress.Api
{
    public class Seance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Exercice> Exercices { get; set; }

        public Seance(string name, List<Exercice> exercices)
        {
            Name = name;
            Exercices = exercices;
        }
        public Seance(List<Exercice> exercices)
        {
            Exercices = exercices;
        }
        public Seance(string name)
        {
            Name = name;
        }
    }
}
