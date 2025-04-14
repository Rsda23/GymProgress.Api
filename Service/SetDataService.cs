using GymProgress.Api.Entities;
using GymProgress.Api.Interface;
using GymProgress.Api.Interface.Map;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class SetDataService : ISetDataService, IMapToList<SetDataEntity, SetData>
    {
        private readonly IMongoDatabase _database;
        private SetDataService(MongoHelper mongoHelpers)
        {
            _database = mongoHelpers.GetDatabase();
        }

        public void CreateFullSetData(string exerciceId, int repetition, int serie, float charge)
        {
            DateTime date = DateTime.Now;
            SetDataEntity setData = new SetDataEntity(exerciceId, repetition, serie, charge, date);

            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            collection.InsertOne(setData);
        }

        public SetData GetSetDataById(string id)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(id);

            SetDataEntity matching = collection.Find(filter).FirstOrDefault();

            return matching.MapToDomain();
        }
        public SetData GetSetDataByExerciceId(string exerciceId)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(exerciceId);

            SetDataEntity matching = collection.Find(filter).FirstOrDefault();

            return matching.MapToDomain();
        }

        public void DeleteSetDataById(string id)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(id);

            SetDataEntity matching = collection.Find(filter).FirstOrDefault();

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<SetDataEntity>(id));
            }
        }

        public void UpdateSetData(string setDataId, int repetition, int serie, float charge)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setDataId);

            var update = Builders<SetDataEntity>.Update.Combine(
                Builders<SetDataEntity>.Update.Set(f => f.Repetition, repetition),
                Builders<SetDataEntity>.Update.Set(f => f.Serie, serie),
                Builders<SetDataEntity>.Update.Set(f => f.Charge, charge));

            collection.UpdateOne(filter, update);
        }
        public void UpdateRepetition(string setdataId, int repetition)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setdataId);

            var update = Builders<SetDataEntity>.Update.Combine(
                Builders<SetDataEntity>.Update.Set(f => f.Repetition, repetition));

            collection.UpdateOne(filter, update);
        }
        public void UpdateSerie(string setdataId, int serie)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setdataId);

            var update = Builders<SetDataEntity>.Update.Combine(
                Builders<SetDataEntity>.Update.Set(f => f.Serie, serie));

            collection.UpdateOne(filter, update);
        }
        public void UpdateCharge(string setdataId, float charge)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setdataId);

            var update = Builders<SetDataEntity>.Update.Combine(
                Builders<SetDataEntity>.Update.Set(f => f.Charge, charge));

            collection.UpdateOne(filter, update);
        }

        public List<SetData> MapToList(List<SetDataEntity> data)
        {
            var result = new List<SetData>();
            foreach (var item in data)
            {
                result.Add(item.MapToDomain());
            }
            return result;
        }
    }
}
