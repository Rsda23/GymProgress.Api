using GymProgress.Api.Interface;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IMongoDatabase _database;

        public UserService(MongoHelper mongoHelper)
        {
            _database = mongoHelper.GetDatabase();
        }

        public void CreateUser(string pseudo, string email, string hashedPassword)
        {
            User user = new User(pseudo, email, hashedPassword);
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<User>("users");
            collection.InsertOne(user);

        }

        public List<User> GetAllUser()
        {
            var collection = _database.GetCollection<User>("users");
            List<User> users = collection.Find(Builders<User>.Filter.Empty).ToList();

            return users;
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

        public void ReplaceUser(string id, string pseudo, string email)
        {
            var collection = _database.GetCollection<User>("users");

            var filter = MongoHelper.BuildFindByIdRequest<User>(id);
            var update = Builders<User>.Update.Combine(
                Builders<User>.Update.Set(f => f.Pseudo, pseudo),
                Builders<User>.Update.Set(f => f.Email, email));

            collection.UpdateOne(filter, update);
        }

        public void ReplaceUserByPassword(string id, string password)
        {
            var collection = _database.GetCollection<User>("users");

            var filter = MongoHelper.BuildFindByIdRequest<User>(id);
            var update = Builders<User>.Update.Set(f => f.HashedPassword, password);

            collection.UpdateOne(filter, update);
        }

    }
}
