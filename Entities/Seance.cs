using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class Seance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ExerciceEntity> Exercices { get; set; }

        public Seance(string name, List<ExerciceEntity> exercices)
        {
            Name = name;
            Exercices = exercices;
        }
        public Seance(List<ExerciceEntity> exercices)
        {
            Exercices = exercices;
        }
        public Seance(string name)
        {
            Name = name;
        }
    }
}
