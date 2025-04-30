using GymProgress.Api.Interface.Map;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        public SeanceEntity()
        {
            Exercices = new List<ExerciceEntity>();
        }

        public SeanceEntity(string name, List<ExerciceEntity> exercices)
        {
            Name = name;
            Exercices = exercices;
        }
        public SeanceEntity(string name, List<ExerciceEntity> exercices, string userId)
        {
            Name = name;
            Exercices = exercices;
            UserId = userId;
        }
        public SeanceEntity(List<ExerciceEntity> exercices)
        {
            Exercices = exercices;
        }
        public SeanceEntity(string name)
        {
            Name = name;
        }
        public SeanceEntity(string name, string userId)
        {
            Name = name;
            UserId = userId;
        }

        public Seance MapToDomain()
        {
            var result = new Seance
            {
                SeanceId = Id,
                Name = Name,
                Exercices = MapExercices(),
                UserId = UserId
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
