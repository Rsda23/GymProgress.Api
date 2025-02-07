using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace GymProgress.Api
{
    public class ExerciceService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;

        public ExerciceService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["GymProgressDatabase:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Error connection MongoDB");
            }
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Gym_Progress");
        }

        public void AddExercice(string nom, int repetition, int serie, float charge, DateTime date)
        {
            Exercice exercice = new Exercice(nom, repetition, serie, charge, date);

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
            
            var builder = Builders<Exercice>.Filter;
            ObjectId objectId = ObjectId.Parse(id);
            var filter = builder.Eq("_id", objectId);

            Exercice matching = collection.Find(filter).FirstOrDefault();

            return matching;
        }

        public Exercice GetExerciceByName(string nom)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<Exercice>("exercices");

            var builder = Builders<Exercice>.Filter.Regex(f => f.Nom, new BsonRegularExpression(nom, "i"));
           

            Exercice matching = collection.Find(builder).FirstOrDefault();

            return matching;
        }
        
        public void DeleteExercice()
        {

        }
    }
}
