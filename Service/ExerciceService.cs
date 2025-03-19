using GymProgress.Api.Interface;
using GymProgress.Api.Interface.Map;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
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

        public void CreateExercice(string nom, int repetition, int serie, float charge, string userId)
        {
            DateTime date = DateTime.Now;
            ExerciceEntity exercice = new ExerciceEntity(nom, repetition, serie, charge, date, userId);

            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            collection.InsertOne(exercice);
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

        public void DeleteExerciceById(string id)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id));
            }
        }

        public void DeleteExerciceByName(string name)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            if (matching == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            collection.DeleteOne(filter);
        }

        public void ReplaceExercice(string id, string name, int repetition, int serie, float charge)
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");

            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);

            var update = Builders<ExerciceEntity>.Update.Combine(
                Builders<ExerciceEntity>.Update.Set(f => f.Nom, name),
                Builders<ExerciceEntity>.Update.Set(f => f.Repetition, repetition),
                Builders<ExerciceEntity>.Update.Set(f => f.Serie, serie),
                Builders<ExerciceEntity>.Update.Set(f => f.Charge, charge));

            collection.UpdateOne(filter, update);
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
