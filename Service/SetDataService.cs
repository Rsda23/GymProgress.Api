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
        private readonly IExerciceService _service;
        public SetDataService(MongoHelper mongoHelpers, IExerciceService service)
        {
            _database = mongoHelpers.GetDatabase();
            _service = service;
        }

        public void CreateFullSetData(string exerciceId, int repetition, int serie, float charge, string userId)
        {
            DateTime date = DateTime.Now;
            SetDataEntity setData = new SetDataEntity(exerciceId, repetition, serie, charge, date, userId);

            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            collection.InsertOne(setData);

            _service.AddSetToExercice(exerciceId, setData);
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
            var filter = Builders<SetDataEntity>.Filter.Eq(s => s.ExerciceId, exerciceId);

            SetDataEntity matching = collection.Find(filter).FirstOrDefault();

            return matching?.MapToDomain();
        }

        public async void DeleteSetDataById(string setDataId)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            var exerciceCollection = _database.GetCollection<ExerciceEntity>("exercices");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setDataId);

            SetDataEntity matching = collection.Find(filter).FirstOrDefault();

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<SetDataEntity>(setDataId));
            }

            await exerciceCollection.UpdateManyAsync(
                Builders<ExerciceEntity>.Filter.ElemMatch(e => e.SetDatas, sd => sd.Id == setDataId),
                Builders<ExerciceEntity>.Update.PullFilter(e => e.SetDatas, sd => sd.Id == setDataId)
);
        }

        public void UpdateSetData(SetData setData)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");

            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setData.SetDataId);

            var update = Builders<SetDataEntity>.Update.Combine(
                Builders<SetDataEntity>.Update.Set(f => f.Repetition, setData.Repetition),
                Builders<SetDataEntity>.Update.Set(f => f.Serie, setData.Serie),
                Builders<SetDataEntity>.Update.Set(f => f.Charge, setData.Charge));

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

        public void ReplaceSetData(SetData setData)
        {
            var collection = _database.GetCollection<SetDataEntity>("setdatas");
            var filter = MongoHelper.BuildFindByIdRequest<SetDataEntity>(setData.SetDataId);

            var existing = collection.Find(filter).FirstOrDefault();

            bool changed = false;

            if (setData.Repetition != existing.Repetition)
            {
                changed = true;
                existing.Repetition = setData.Repetition;
            }
            if (setData.Serie != existing.Serie)
            {
                changed = true;
                existing.Serie = setData.Serie;
            }
            if (setData.Charge != existing.Charge)
            {
                changed = true;
                existing.Charge = setData.Charge;
            }

            if (changed)
            {
                collection.ReplaceOne(filter, existing);

                _service.ReplaceSetToExercice(setData.ExerciceId, existing);
            }
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
