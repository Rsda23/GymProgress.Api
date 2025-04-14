using GymProgress.Api.Interface.Map;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Xml.Linq;

namespace GymProgress.Api.Entities
{
    public class SetDataEntity : IMapToDomain<SetData>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("setDataId")]
        public string SetDataId { get; set; } = string.Empty;
        [BsonElement("Repetition")]
        public int Repetition { get; set; }
        [BsonElement("Serie")]
        public int Serie { get; set; }
        [BsonElement("Charge")]
        public float Charge { get; set; }
        [BsonElement("Date")]
        public DateTime Date { get; set; }

        public SetDataEntity()
        {

        }
        public SetDataEntity(int repetition, int serie, float charge, DateTime date)

        {
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
        }

        public SetData MapToDomain()
        {
            var result = new SetData
            {
                SetDataId = Id,
                Repetition = Repetition,
                Serie = Serie,
                Charge = Charge,
                Date = Date
            };

            return result;
        }
    }
}
