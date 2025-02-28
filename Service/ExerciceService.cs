using GymProgress.Api.Interface;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class ExerciceService : IExerciceService
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

        public List<ExerciceEntity> GetAllExercice()
        {
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            List<ExerciceEntity> exercices = collection.Find(Builders<ExerciceEntity>.Filter.Empty).ToList();

            return exercices;
        }

        public ExerciceEntity GetExerciceById(string id)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            return matching;
        }

        public ExerciceEntity GetExerciceByName(string name)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);

            ExerciceEntity matching = collection.Find(filter).FirstOrDefault();

            return matching;
        }

        public void DeleteExerciceById(string id)
        {
            ExerciceEntity matching = GetExerciceById(id);

            var collection = _database.GetCollection<ExerciceEntity>("exercices");

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id));
            }
        }

        public void DeleteExerciceByName(string name)
        {
            ExerciceEntity matching = GetExerciceByName(name);
            if (matching == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<ExerciceEntity>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);

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
    }


}
