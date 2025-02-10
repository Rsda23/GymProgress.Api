using GymProgress.Api.MongoHelpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymProgress.Api
{
    public class UserService
    {
        private readonly IMongoDatabase _database;

        public UserService(MongoHelper mongoHelper)
        {
            _database = mongoHelper.GetDatabase();
        }

        public void PostUser(string pseudo, string email, string password)
        {
            var collection = _database.GetCollection<User>("users");

            User user = new User(pseudo, email, password);
            collection.InsertOne(user);

        }
        public User GetUserById(string id)
        {
            var collection = _database.GetCollection<User>("users");

            var filter = MongoHelper.BuildFindByIdRequestId<User>(id);
            User user = collection.Find(filter).FirstOrDefault();

            return user;
        }

        //Passer dans MongoHelper
        public User GetUserByName(string name)
        {
            var collection = _database.GetCollection<User>("users");

            var builder = Builders<User>.Filter.Regex(f => f.Pseudo, new BsonRegularExpression(name, "i"));
            User user = collection.Find(builder).FirstOrDefault();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var collection = _database.GetCollection<User>("users");

            var builder = Builders<User>.Filter.Regex(f => f.Email, new BsonRegularExpression(email, "i"));
            User user = collection.Find(builder).FirstOrDefault();

            return user;
        }

    }
}
