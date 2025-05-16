using GymProgress.Api.Interface;
using GymProgress.Api.Interface.Map;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class SeanceService : ISeanceService, IMapToList<SeanceEntity, Seance>
    {
        private readonly IMongoDatabase _database;
        private readonly IExerciceService _exerciceService;

        public SeanceService(MongoHelper mongoHelper, IExerciceService exerciceService)
        {
            _database = mongoHelper.GetDatabase();
            _exerciceService = exerciceService;
        }

        public void CreateSeance(string name, string userId)
        {
            DateTime date = DateTime.Now;
            SeanceEntity seance = new SeanceEntity(name, userId, date);

            var collection = _database.GetCollection<SeanceEntity>("seances");
            collection.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceId(string nameSeance, List<string> exerciceId, string userId)
        {
            DateTime date = DateTime.Now;
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            if (exerciceId != null && exerciceId.Count() > 0)
            {
                foreach (var id in exerciceId)
                {
                    var filter = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);
                    var exercice = collectionExercice.Find(filter).FirstOrDefault();

                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            SeanceEntity seance = new SeanceEntity(nameSeance, exercices, userId, date);

            collectionSeance.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceName(string nameSeance, List<string> exerciceName, string userId)
        {
            DateTime date = DateTime.Now;
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();

            if (exerciceName != null && exerciceName.Count() > 0)
            {
                foreach (var name in exerciceName)
                {
                    var filter = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);
                    var exercice = collectionExercice.Find(filter).FirstOrDefault();

                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }
            
            SeanceEntity seance = new SeanceEntity(nameSeance, exercices, userId, date);
            collectionSeance.InsertOne(seance);
        }
        public void AddExerciceToSeanceById(string seanceId, List<string> execiceId)
        {
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            SeanceEntity oldSeance = collectionSeance.Find(filter).FirstOrDefault();

            List<ExerciceEntity> oldExercice = oldSeance.Exercices;
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();

            if (execiceId != null && execiceId.Count() > 0)
            {
                foreach (var id in execiceId)
                {
                    var filterExercice = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);
                    var exercice = collectionExercice.Find(filterExercice).FirstOrDefault();

                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            oldExercice.AddRange(exercices);
            
            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, oldExercice);
            collectionSeance.UpdateOne(filter, update);
        }
        public void AddExerciceToSeanceByName(string seanceId, List<string> execiceName)
        {
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            SeanceEntity oldSeance = collectionSeance.Find(filter).FirstOrDefault();

            List<ExerciceEntity> oldExercice = oldSeance.Exercices;
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();

            if (execiceName != null && execiceName.Count() > 0)
            {
                foreach (var name in execiceName)
                {
                    var filterExercice = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);
                    var exercice = collectionExercice.Find(filterExercice).FirstOrDefault();

                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            oldExercice.AddRange(exercices);

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, oldExercice);
            collectionSeance.UpdateOne(filter, update);
        }

        public List<Seance> GetAllSeance()
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");

            var result = new List<Seance>();

            List<SeanceEntity> seances = collection
                .Find(Builders<SeanceEntity>.Filter.Empty)
                .ToList();
            
            return MapToList(seances);
        }
        public Seance GetSeanceById(string id)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(id);

            SeanceEntity seance = collection.Find(filter).FirstOrDefault();
            
            return seance.MapToDomain();
        }
        public Seance GetSeanceByName(string name)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("Name", name);

            SeanceEntity seance = collection.Find(filter).FirstOrDefault();

            return seance.MapToDomain();

        }
        public List<Seance> GetSeanceByUserId(string userId)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("UserId", userId);

            List<SeanceEntity> seances = collection
                .Find(filter)
                .ToList();

            return MapToList(seances);
        }
        public List<Seance> GetSeancePublic()
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("UserId", "string");

            List<SeanceEntity> seances = collection
                .Find(filter)
                .ToList();

            return MapToList(seances);
        }
        public List<Seance> GetLastSeance(int count, string userId)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("UserId", userId);

            List<SeanceEntity> seances = collection
                .Find(filter)
                .SortByDescending(s => s.Date)
                .Limit(count)
                .ToList();

            return MapToList(seances);
        }

        public void DeleteSeanceById(string id)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(id);
            collection.DeleteOne(filter);
        }
        public void DeleteSeanceByName(string name)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("Name", name);
            collection.DeleteOne(filter);
        }
        public void DeleteExerciceToSeanceById(string seanceId, List<string> exerciceId)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            SeanceEntity seance = collection.Find(seanceId).FirstOrDefault();

            if (exerciceId != null && exerciceId.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceId.Contains(f.Id));
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, seance.Exercices);

            collection.UpdateOne(filter, update);
        }
        public void DeleteExerciceToSeanceByName(string seanceId, List<string> exerciceName)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);
            SeanceEntity seance = collection.Find(seanceId).FirstOrDefault();

            if (exerciceName != null && exerciceName.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceName.Contains(f.Nom));
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, seance.Exercices);

            collection.UpdateOne(filter, update);
        }
        public async void DeleteAllSeance()
        {
            var collectionSeances = _database.GetCollection<SeanceEntity>("seances");

            await collectionSeances.DeleteManyAsync(Builders<SeanceEntity>.Filter.Empty);
        }

        public async void RemoveExerciceToSeance(string seanceId, string exerciceId)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");

            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            var update = Builders<SeanceEntity>.Update.PullFilter(s => s.Exercices, e => e.Id == exerciceId);

            await collection.UpdateOneAsync(filter, update);
        }

        public void UpdateDate(string seanceId)
        {
            DateTime date = DateTime.Now;
            var collection = _database.GetCollection<SeanceEntity>("seances");

            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            var update = Builders<SeanceEntity>.Update.Combine(
                Builders<SeanceEntity>.Update.Set(f => f.Date, date));

            collection.UpdateOne(filter, update);
        }
        public void ReplaceSeance(string id, string name)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(id);

            var update = Builders<SeanceEntity>.Update.Set(f => f.Name, name);
            collection.UpdateOne(filter, update);
        }
        public void ReplaceExerciceById(string seanceId, List<string> exerciceId)
        {
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            foreach (var id in exerciceId)
            {
                var filterExercice = MongoHelper.BuildFindByIdRequest<ExerciceEntity>(id);
                var exercice = collectionExercice.Find(filterExercice).FirstOrDefault();

                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, exercices);
            collectionSeance.UpdateOne(filter, update);
        }
        public void ReplaceExerciceByName(string seanceId, List<string> exerciceName)
        {
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            foreach (var name in exerciceName)
            {
                var filterExercice = MongoHelper.BuildFindByChampRequest<ExerciceEntity>("Nom", name);
                var exercice = collectionExercice.Find(filterExercice).FirstOrDefault();

                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, exercices);
            collectionSeance.UpdateOne(filter, update);
        }

        public List<Seance> MapToList(List<SeanceEntity> data)
        {
            var result = new List<Seance>();
            foreach (var item in data)
            {
                result.Add(item.MapToDomain());
            }
            return result;
        }
    }
}
