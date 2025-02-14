using MongoDB.Bson;
using MongoDB.Driver;

namespace GymProgress.Api.MongoHelpers
{
    public class MongoHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        public MongoHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["GymProgressDatabase:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Error connection MongoDB");
            }
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("GymProgress");
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FilterDefinition<T> BuildFindByIdRequest<T>(string id)
        {
            var builder = Builders<T>.Filter;
            ObjectId objectId = ObjectId.Parse(id);
            var filter = builder.Eq("_id", objectId);
            return filter;
        }

        public static FilterDefinition<T> BuildFindByChampRequest<T>(string champ, string value)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(champ, value);
            return filter;
        }


    }
}
