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

            var filter = MongoHelper.BuildFindByIdRequest<User>(id);
            User user = collection.Find(filter).FirstOrDefault();

            return user;
        }

        public User GetUserByPseudo(string pseudo)
        {
            var collection = _database.GetCollection<User>("users");

            var filter = MongoHelper.BuildFindByChampRequest<User>("Pseudo", pseudo);
            User user = collection.Find(filter).FirstOrDefault();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var collection = _database.GetCollection<User>("users");

            var builder = MongoHelper.BuildFindByChampRequest<User>("Email", email);
            User user = collection.Find(builder).FirstOrDefault();

            return user;
        }

        public void DeleteUserById(string id)
        {
            var collection = _database.GetCollection<User>("users");
            var filter = MongoHelper.BuildFindByIdRequest<User>(id);

            collection.DeleteOne(filter);
        }

        public void DeleteUserByEmail(string email)
        {
            var collection = _database.GetCollection<User>("users");
            var filter = MongoHelper.BuildFindByChampRequest<User>("Email", email);

            collection.DeleteOne(filter);
        }
    }
}
