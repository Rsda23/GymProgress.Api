using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

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
