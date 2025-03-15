using GymProgress.Api.Interface;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class ExerciceEntity : IMapToDomain<Exercice> 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("exerciceId")]
        public string ExerciceId { get; set; } = string.Empty;
        [BsonElement("Nom")]
        public string Nom { get; set; }
        [BsonElement("Repetition")]
        public int Repetition { get; set; }
        [BsonElement("Serie")]
        public int Serie { get; set; }
        [BsonElement("Charge")]
        public float Charge { get; set; }
        [BsonElement("Date")]
        public DateTime Date { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }

        public ExerciceEntity()
        {
            
        }
        public ExerciceEntity(string nom, int repetition, int serie, float charge, DateTime date, string userId) 
                       
        {
            Nom = nom;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
            UserId = userId;
        }

        public Exercice MapToDomain()
        {
            Exercice result = new Exercice
            {
                ExerciceId = Id,
                Nom = Nom,
                Repetition = Repetition,
                Serie = Serie,
                Charge = Charge,
                Date = Date,
                UserId = UserId
            };
            return result;
        }

        //public Exercice Map()
        //{
        //    Exercice result = new Exercice
        //    {
        //        ExerciceId = Id,
        //        Nom = Nom,
        //        Repetition = Repetition,
        //        Serie = Serie,
        //        Charge = Charge,
        //        Date = Date,
        //        UserId = UserId
        //    };
        //    return result;
        //}
    }
}
