using GymProgress.Api.Entities;
using GymProgress.Api.Interface.Map;
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
        [BsonElement("UserId")]
        public string UserId { get; set; }
        public List<SetData> SetDatas { get; set; } = [];


        public ExerciceEntity()
        {
            
        }
        public ExerciceEntity(string nom)
        {
            Nom = nom;
        }
        public ExerciceEntity(string nom, string userId, List<SetData> setDatas) 
                       
        {
            Nom = nom;
            UserId = userId;
            SetDatas = setDatas;
        }

        public Exercice MapToDomain()
        {
            Exercice result = new Exercice
            {
                ExerciceId = Id,
                Nom = Nom,
                UserId = UserId,
                SetDatas = MapSetDatas()
            };
            return result;
        }

        private List<SetData> MapSetDatas()
        {
            var result = new List<SetData>();
            if (SetDatas != null)
            {
                foreach (var item in SetDatas)
                {
                    result.Add(new SetData(item.Repetition, item.Serie, item.Charge, item.Date));
                }
            }
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
