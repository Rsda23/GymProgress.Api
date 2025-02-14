using GymProgress.Api.MongoHelpers;
using MongoDB.Driver;
using System.Linq;
using System.Xml.Linq;

namespace GymProgress.Api
{
    public class SeanceService : ISeanceService
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
            Seance seance = new Seance(name);

            var collection = _database.GetCollection<Seance>("seances");
            collection.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceId(string nameSeance, List<string> exerciceId)
        {
            List<Exercice> exercices = new List<Exercice>();
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

            Seance seance = new Seance(nameSeance, exercices);

            var collection = _database.GetCollection<Seance>("seances");
            collection.InsertOne(seance);

        }
        public void CreateSeanceWithExerciceName(string nameSeance, List<string> exerciceName)
        {
            List<Exercice> exercices = new List<Exercice>();
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

            Seance seance = new Seance(nameSeance, exercices);
            var collection = _database.GetCollection<Seance>("seances");
            collection.InsertOne(seance);
        }
        public void AddExerciceToSeanceById(string seanceId, List<string> execiceId)
        {
            List<Exercice> exercices = new List<Exercice>();
            Seance oldSeance = GetSeanceById(seanceId);
            List<Exercice> oldExercice = oldSeance.Exercices;

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

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(seanceId);
            var update = Builders<Seance>.Update.Set(f => f.Exercices, oldExercice);
            collection.UpdateOne(filter, update);
        }
        public void AddExerciceToSeanceByName(string seanceId, List<string> execiceName)
        {
            List<Exercice> exercices = new List<Exercice>();
            Seance oldSeance = GetSeanceById(seanceId);
            List<Exercice> oldExercice = oldSeance.Exercices;

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

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(seanceId);
            var update = Builders<Seance>.Update.Set(f => f.Exercices, oldExercice);
            collection.UpdateOne(filter, update);
        }

        public Seance GetSeanceById(string id)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            Seance seance = collection.Find(filter).FirstOrDefault();
            return seance;
        }
        public Seance GetSeanceByName(string name)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<Seance>("Name", name);
            Seance seance = collection.Find(filter).FirstOrDefault();
            return seance;

        }

        public void DeleteSeanceById(string id)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            collection.DeleteOne(filter);
        }
        public void DeleteSeanceByName(string name)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByChampRequest<Seance>("Name", name);
            collection.DeleteOne(filter);
        }
        public void DeleteExerciceToSeanceById(string Seanceid, List<string> exerciceId)
        {
            Seance seance = GetSeanceById(Seanceid);

            if (exerciceId !=null && exerciceId.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceId.Contains(f.Id));
            }

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(Seanceid);
            var update = Builders<Seance>.Update.Set(f => f.Exercices, seance.Exercices);

            collection.UpdateOne(filter, update);
        }
        public void DeleteExerciceToSeanceByName(string Seanceid, List<string> exerciceName)
        {
            Seance seance = GetSeanceById(Seanceid);

            if (exerciceName != null && exerciceName.Count() > 0)
            {
                seance.Exercices.RemoveAll(f => exerciceName.Contains(f.Nom));
            }

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(Seanceid);
            var update = Builders<Seance>.Update.Set(f => f.Exercices, seance.Exercices);

            collection.UpdateOne(filter, update);
        }

        public void ReplaceSeance(string id, string name)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            var update = Builders<Seance>.Update.Set(f => f.Name, name);
            collection.UpdateOne(filter, update);
        }
        public void ReplaceExerciceById(string seanceId, List<string> exerciceId)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(seanceId);

            List<Exercice> exercices = new List<Exercice>();
            foreach (var id in exerciceId)
            {
                var exercice = _exerciceService.GetExerciceById(id);
                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<Seance>.Update.Set(f => f.Exercices, exercices);
            collection.UpdateOne(filter, update);
        }
        public void ReplaceExerciceByName(string seanceId, List<string> exerciceName)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(seanceId);

            List<Exercice> exercices = new List<Exercice>();
            foreach (var name in exerciceName)
            {
                var exercice = _exerciceService.GetExerciceByName(name);
                if (exercice != null)
                {
                    exercices.Add(exercice);
                }
            }

            var update = Builders<Seance>.Update.Set(f => f.Exercices, exercices);
            collection.UpdateOne(filter, update);
        }
    }
}
