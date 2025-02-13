using GymProgress.Api.MongoHelpers;
using MongoDB.Driver;
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

        public void PostSeanceWithExerciceId (string name, List<string> exerciceId)
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

            Seance seance = new Seance(name, exercices);

            var collection = _database.GetCollection<Seance>("seances");
            collection.InsertOne(seance);

        }

        public void PostSeance(string name)
        {
            Seance seance = new Seance(name);

            var collection = _database.GetCollection<Seance>("seances");
            collection.InsertOne(seance);

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

        public void PutSeance(string id, string name, List<Exercice> exercices)
        {
            Seance oldSeance = GetSeanceById(id);
            List<Exercice> oldExercice = oldSeance.Exercices;
            List<Exercice> fusion = oldExercice.Concat(exercices).ToList();

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            var update = Builders<Seance>.Update.Combine(
                Builders<Seance>.Update.Set(f => f.Name, name),
                Builders<Seance>.Update.Set(f => f.Exercices, fusion));
            
            collection.UpdateOne(filter, update);
        }

        public void PutSeanceName(string id, string name)
        {
            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            var update = Builders<Seance>.Update.Set(f => f.Name, name);
            collection.UpdateOne(filter, update);
        }

        public void PutSeanceExercice(string id, List<Exercice> exercices)
        {
            Seance oldSeance = GetSeanceById(id);
            List<Exercice> oldExercice = oldSeance.Exercices;
            List<Exercice> Fusion = oldExercice.Concat(exercices).ToList();

            var collection = _database.GetCollection<Seance>("seances");
            var filter = MongoHelper.BuildFindByIdRequest<Seance>(id);
            var update = Builders<Seance>.Update.Set(f => f.Exercices, Fusion);
            collection.UpdateOne(filter, update);
        }
    }
}
