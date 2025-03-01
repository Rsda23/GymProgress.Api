using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class SeanceEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ExerciceEntity> Exercices { get; set; }

        public SeanceEntity(string name, List<ExerciceEntity> exercices)
        {
            Name = name;
            Exercices = exercices;
        }
        public SeanceEntity(List<ExerciceEntity> exercices)
        {
            Exercices = exercices;
        }
        public SeanceEntity(string name)
        {
            Name = name;
        }

        public Seance MapTo()
        {
            var result = new Seance
            {
                Exercices = MapExercices(),
                Name = Name,
                SeanceId = Id
            };

            return result;
        }

        private List<Exercice> MapExercices()
        {
            var result = new List<Exercice>();
            foreach (var item in Exercices)
            {
                result.Add(new Exercice(item.Nom, item.Repetition, item.Serie, item.Charge, item.Date, item.UserId));
            }
            return result;
        }
    }
}
