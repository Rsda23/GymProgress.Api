using GymProgress.Api.Interface;
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

        public void CreateSeance(string name)
        {
            SeanceEntity seance = new SeanceEntity(name);

            var collection = _database.GetCollection<SeanceEntity>("seances");
            collection.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceId(string nameSeance, List<string> exerciceId)
        {
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            if (exerciceId != null && exerciceId.Count() > 0)
            {
                foreach (var id in exerciceId)
                {
                    var exercice = _exerciceService.GetExerciceById(id);
                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            SeanceEntity seance = new SeanceEntity(nameSeance, exercices);

            var collection = _database.GetCollection<SeanceEntity>("seances");
            collection.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceName(string nameSeance, List<string> exerciceName)
        {
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            if (exerciceName != null && exerciceName.Count() > 0)
            {
                foreach (var name in exerciceName)
                {
                    var exercice = _exerciceService.GetExerciceByName(name);
                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            SeanceEntity seance = new SeanceEntity(nameSeance, exercices);
            var collection = _database.GetCollection<SeanceEntity>("seances");
            collection.InsertOne(seance);
        }
        public void AddExerciceToSeanceById(string seanceId, List<string> execiceId)
        {
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            SeanceEntity oldSeance = GetSeanceById(seanceId);
            List<ExerciceEntity> oldExercice = oldSeance.Exercices;

            if (execiceId != null && execiceId.Count() > 0)
            {
                foreach (var id in execiceId)
                {
                    var exercice = _exerciceService.GetExerciceById(id);
                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            oldExercice.AddRange(exercices);

            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);
            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, oldExercice);
            collection.UpdateOne(filter, update);
        }
        public void AddExerciceToSeanceByName(string seanceId, List<string> execiceName)
        {
            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            SeanceEntity oldSeance = GetSeanceById(seanceId);
            List<ExerciceEntity> oldExercice = oldSeance.Exercices;

            if (execiceName != null && execiceName.Count() > 0)
            {
                foreach (var name in execiceName)
                {
                    var exercice = _exerciceService.GetExerciceByName(name);
                    if (exercice != null)
                    {
                        exercices.Add(exercice);
                    }
                }
            }

            oldExercice.AddRange(exercices);

            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);
            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, oldExercice);
            collection.UpdateOne(filter, update);
        }

        public List<Seance> GetAllSeance()
        {
            var result = new List<Seance>();
            var collection = _database.GetCollection<SeanceEntity>("seances");
            List<SeanceEntity> seances = collection
                                            .Find(Builders<SeanceEntity>.Filter.Empty)
                                            .ToList();
            
            return MapToList(seances);
        }
        public SeanceEntity GetSeanceById(string id)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(id);
            SeanceEntity seance = collection.Find(filter).FirstOrDefault();
            return seance;
        }
        public SeanceEntity GetSeanceByName(string name)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<SeanceEntity>("Name", name);
            SeanceEntity seance = collection.Find(filter).FirstOrDefault();
            return seance;

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
        public void DeleteExerciceToSeanceById(string Seanceid, List<string> exerciceId)
        {
            SeanceEntity seance = GetSeanceById(Seanceid);

            if (exerciceId != null && exerciceId.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceId.Contains(f.Id));
            }

            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(Seanceid);
            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, seance.Exercices);

            collection.UpdateOne(filter, update);
        }
        public void DeleteExerciceToSeanceByName(string Seanceid, List<string> exerciceName)
        {
            SeanceEntity seance = GetSeanceById(Seanceid);

            if (exerciceName != null && exerciceName.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceName.Contains(f.Nom));
            }

            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(Seanceid);
            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, seance.Exercices);

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
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            foreach (var id in exerciceId)
            {
                var exercice = _exerciceService.GetExerciceById(id);
                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, exercices);
            collection.UpdateOne(filter, update);
        }
        public void ReplaceExerciceByName(string seanceId, List<string> exerciceName)
        {
            var collection = _database.GetCollection<SeanceEntity>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<SeanceEntity>(seanceId);

            List<ExerciceEntity> exercices = new List<ExerciceEntity>();
            foreach (var name in exerciceName)
            {
                var exercice = _exerciceService.GetExerciceByName(name);
                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<SeanceEntity>.Update.Set(f => f.Exercices, exercices);
            collection.UpdateOne(filter, update);
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
