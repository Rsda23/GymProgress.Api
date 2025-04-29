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
        [BsonElement("nom")]
        public string Nom { get; set; }
        [BsonElement("userId")]
        public string UserId { get; set; }
        [BsonElement("setDatas")]
        public List<SetDataEntity> SetDatas { get; set; } = [];


        public ExerciceEntity()
        {
            
        }
        public ExerciceEntity(string nom)
        {
            Nom = nom;
        }
        public ExerciceEntity(string nom, string userId)
        {
            Nom = nom;
            UserId = userId;
        }
        public ExerciceEntity(string nom, string userId, List<SetDataEntity> setDatas) 
                       
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
                    result.Add(new SetData(item.Id, item.ExerciceId,item.Repetition, item.Serie, item.Charge, item.Date));
                }
            }
            return result;
        }
    }
}
