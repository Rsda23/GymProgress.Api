using GymProgress.Api.Entities;
using GymProgress.Api.Interface;
using GymProgress.Api.Interface.Map;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class ExerciceService : IExerciceService, IMapToList<ExerciceEntity, Exercice>
    {
        private readonly IMongoDatabase _database;
        public ExerciceService(MongoHelper mongoHelpers)
        {
            _database = mongoHelpers.GetDatabase();
        }

        public void CreateExercice(string nom, string userId)
        {
            DateTime date = DateTime.Now;
            ExerciceEntity exercice = new ExerciceEntity(nom, userId, date);

            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            collection.InsertOne(exercice);
        }

        public void AddSetDataById(string exerciceId, List<string> setDataId)
        {
            var collectionSetdata = _database.GetCollection<SetDataEntity>("setdatas");
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(exerciceId);

            ExerciceEntity oldExercice = collectionExercice.Find(filter).FirstOrDefault();

            List<SetDataEntity> oldSetData = oldExercice.SetDatas;
            List<SetDataEntity> setDatas = new List<SetDataEntity>();

            if (setDataId != null && setDataId.Count() > 0)
            {
                foreach (var id in setDataId)
                {
                    var filterSetData = MongoHelper.BuildFindByIdRequest<SetDataEntity>(id);
                    var setData = collectionSetdata.Find(filterSetData).FirstOrDefault();

                    if (setData != null)
                    {
                        setDatas.Add(setData);
                    }
                }
            }

            oldSetData.AddRange(setDatas);

            var update = Builders<ExerciceEntity>.Update.Set(f => f.SetDatas, oldSetData);
            collectionExercice.UpdateOne(filter, update);
        }

        public List<Exercice> GetAllExercice()
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            List<ExerciceEntity> exercices = collection.Find(Builders<ExerciceEntity>.Filter.Empty).ToList();

            return MapToList(exercices);
        }
        public Exercice GetExerciceById(string id)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            return matching.MapToDomain();
        }
        public Exercice GetExerciceByName(string name)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            return matching.MapToDomain();
        }
        public List<Exercice> GetExercicePublic()
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("UserId", "string");

            List<ExerciceEntity> seances = collection
                .Find(filter)
                .ToList();

            return MapToList(seances);
        }
        public List<Exercice> GetExerciceUserId(string userId)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("UserId", userId);

            List<ExerciceEntity> exercices = collection
                .Find(filter)
                .ToList();

            return MapToList(exercices);
        }
        public async Task<List<Exercice>> GetExercicesBySeanceId(string seanceId)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<SeanceEntity>("seances");

            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            var seance = await collection.Find(filter).FirstOrDefaultAsync();

            if (seance == null || seance.Exercices == null)
                return new List<Exercice>();

            return MapToList(seance.Exercices);
        }

        public async void DeleteExerciceById(string exerciceId)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var setDataCollection = _database.GetCollection<SetDataEntity>("setdatas");
            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(exerciceId);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<ExerciceEntity>(exerciceId));
            }

            await setDataCollection.DeleteManyAsync(s => s.ExerciceId == exerciceId);
        }
        public async void DeleteAllExercice()
        {
            var collectionExercices = _database.GetCollection<ExerciceEntity>("exercices");
            var setDataCollection = _database.GetCollection<SetDataEntity>("setdatas");

            var exercices = GetAllExercice();
            List<string> exercicesId = exercices.Select(e => e.ExerciceId).ToList();
           
            await collectionExercices.DeleteManyAsync(Builders<ExerciceEntity>.Filter.Empty);
            await setDataCollection.DeleteManyAsync(Builders<SetDataEntity>.Filter.In(s => s.ExerciceId, exercicesId));
        }

        public void UpdateName(string exerciceId, string name)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");

            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(exerciceId);

            var update = Builders<ExerciceEntity>.Update.Combine(
                Builders<ExerciceEntity>.Update.Set(f => f.Nom, name));

            collection.UpdateOne(filter, update);
        }

        public async Task AddSetToExercice(string exerciceId, SetDataEntity setData)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");

            var filter = Builders<ExerciceEntity>.Filter.Eq(e => e.Id, exerciceId);
            var update = Builders<ExerciceEntity>.Update.Push(e => e.SetDatas, setData);

            await collection.UpdateOneAsync(filter, update);
        }

        public async Task ReplaceSetToExercice(string exerciceId, SetDataEntity setData)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");

            var filter = Builders<ExerciceEntity>.Filter.Eq(e => e.Id, exerciceId);
            var update = Builders<ExerciceEntity>.Update.Set("SetDatas.$[elem]", setData);

            ObjectId objectId = ObjectId.Parse(setData.Id);

            var arrayFilter = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem._id", 
                new BsonDocument("$eq", objectId)))
            };

            var options = new UpdateOptions { ArrayFilters = arrayFilter };

            await collection.UpdateOneAsync(filter, update, options);
        }

        public List<Exercice> MapToList(List<ExerciceEntity> data)
        {
            var result = new List<Exercice>();
            foreach (var item in data)
            {
                result.Add(item.MapToDomain());
            }
            return result;
        }
    }
}
