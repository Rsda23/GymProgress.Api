using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GymProgress.Api
{
    public class Exercice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nom { get; set; }
        public int Repetition { get; set; }
        public int Serie { get; set; }
        public float Charge { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        public Exercice(string nom, int repetition, int serie, float charge, DateTime date, string userId)
        {
            Nom = nom;
            Repetition = repetition;
            Serie = serie;
            Charge = charge;
            Date = date;
            UserId = userId;
        }
    }
}
