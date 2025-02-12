using GymProgress.Api.MongoHelpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymProgress.Api
{
    public class ExerciceService : IExerciceService
    {
        
        private readonly IMongoDatabase _database;

        public ExerciceService(MongoHelper mongoHelpers)
        {
            _database = mongoHelpers.GetDatabase();
        }

        public void PostExercice(string nom, int repetition, int serie, float charge, string userId)
        {
            DateTime date = DateTime.Now;
            Exercice exercice = new Exercice(nom, repetition, serie, charge, date, userId);

            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<Exercice>("exercices");
            collection.InsertOne(exercice);
        }

        public Exercice GetExerciceById(string id)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }
            var collection = _database.GetCollection<Exercice>("exercices");
            var filter = MongoHelper.BuildFindByIdRequest<Exercice>(id);

            Exercice matching = collection.Find(filter).FirstOrDefault();

            return matching;
        }

        public Exercice GetExerciceByName(string name)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<Exercice>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<Exercice>("Nom", name);
           
            Exercice matching = collection.Find(filter).FirstOrDefault();

            return matching;
        }
        
        public void DeleteExerciceById(string id)
        {
            Exercice matching = GetExerciceById(id);

            var collection = _database.GetCollection<Exercice>("exercices");

            if (matching != null)
            {
                collection.DeleteOne(MongoHelper.BuildFindByIdRequest<Exercice>(id));
            }
        }

        public void DeleteExerciceByName(string name)
        {
            Exercice matching = GetExerciceByName(name);
            if (matching == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<Exercice>("exercices");
            var filter = MongoHelper.BuildFindByChampRequest<Exercice>("Nom", name);

            collection.DeleteOne(filter);
        }

        public void PutExerice()
        {
           
        }
    }

    
}
