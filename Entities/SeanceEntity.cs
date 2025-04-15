using GymProgress.Api.Interface.Map;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class SeanceEntity : IMapToDomain<Seance>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("exercices")]
        public List<ExerciceEntity> Exercices { get; set; } = [];

        public SeanceEntity()
        {
            Exercices = new List<ExerciceEntity>();
        }

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

        public Seance MapToDomain()
        {
            var result = new Seance
            {
                SeanceId = Id,
                Name = Name,
                Exercices = MapExercices()
            };

            return result;
        }

        private List<Exercice> MapExercices()
        {
            var result = new List<Exercice>();
            if (Exercices != null)
            {
                foreach (var item in Exercices)
                {
                    var mapSetDatas = item.SetDatas.Select(x => x.MapToDomain()).ToList();
                    result.Add(new Exercice(item.Nom, item.UserId, mapSetDatas));
                }
            }
            return result;
        }
    }
}
