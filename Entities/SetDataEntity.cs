using GymProgress.Api.Interface.Map;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GymProgress.Api.Entities
{
    public class SetDataEntity : IMapToDomain<SetData>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("exerciceId")]
        public string ExerciceId { get; set; } = string.Empty;
        [BsonElement("repetition")]
        public int Repetition { get; set; }
        [BsonElement("serie")]
        public int Serie { get; set; }
        [BsonElement("charge")]
        public float Charge { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        public SetDataEntity()
        {

        }
        public SetDataEntity(string exercicdeId, int repetition, int serie, float charge, DateTime date)
        {
            ExerciceId = exercicdeId;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
        }

        public SetDataEntity(string id, string exerciceId, int repetition, int serie, float charge, DateTime date)
        {
            Id = id;
            ExerciceId = exerciceId;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
        }

        public SetDataEntity(string exercicdeId, int repetition, int serie, float charge, DateTime date, string userId)
        {
            ExerciceId = exercicdeId;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
            UserId = userId;
        }

        public SetDataEntity(string id, string exerciceId, int repetition, int serie, float charge, DateTime date, string userId)
        {
            Id = id;
            ExerciceId = exerciceId;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
            UserId = userId;
        }

        public SetData MapToDomain()
        {
            var result = new SetData
            {
                SetDataId = Id,
                Repetition = Repetition,
                Serie = Serie,
                Charge = Charge,
                Date = Date,
                UserId = UserId
            };

            return result;
        }
    }
}
