using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class ExerciceEntity : Exercice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        

        public ExerciceEntity(string nom, int repetition, int serie, float charge, DateTime date, string userId) 
                       : base(nom, repetition, serie, charge, date, userId)
        {
            ExerciceId = Id;
        }
    }
}
