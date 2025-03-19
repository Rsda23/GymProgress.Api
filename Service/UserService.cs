using GymProgress.Api.Interface;
using GymProgress.Api.Interface.Map;
using GymProgress.Api.Models;
using GymProgress.Api.MongoHelpers;
using GymProgress.Domain.Models;
using MongoDB.Driver;

namespace GymProgress.Api.Service
{
    public class UserService : IUserService, IMapToList<UserEntity, User>
    {
        private readonly IMongoDatabase _database;

        public UserService(MongoHelper mongoHelper)
        {
            _database = mongoHelper.GetDatabase();
        }

        public void CreateUser(string pseudo, string email, string hashedPassword)
        {
            UserEntity user = new UserEntity(pseudo, email, hashedPassword);
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<UserEntity>("users");
            collection.InsertOne(user);

        }

        public List<User> GetAllUser()
        {
            var collection = _database.GetCollection<UserEntity>("users");
            List<UserEntity> users = collection.Find(Builders<UserEntity>.Filter.Empty).ToList();

            return MapToList(users);
        }
        public User GetUserById(string id)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(id);
            UserEntity user = collection.Find(filter).FirstOrDefault();

            return user.MapToDomain();
        }

        public User GetUserByPseudo(string pseudo)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByChampRequest<UserEntity>("Pseudo", pseudo);
            UserEntity user = collection.Find(filter).FirstOrDefault();

            return user.MapToDomain();
        }

        public User GetUserByEmail(string email)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var builder = MongoHelper.BuildFindByChampRequest<UserEntity>("Email", email);
            UserEntity user = collection.Find(builder).FirstOrDefault();

            return user.MapToDomain();
        }

        public void DeleteUserById(string id)
        {
            var collection = _database.GetCollection<UserEntity>("users");
            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(id);

            collection.DeleteOne(filter);
        }

        public void DeleteUserByEmail(string email)
        {
            var collection = _database.GetCollection<UserEntity>("users");
            var filter = MongoHelper.BuildFindByChampRequest<UserEntity>("Email", email);

            collection.DeleteOne(filter);
        }

        public void ReplaceUser(string id, string pseudo, string email)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(id);
            var update = Builders<UserEntity>.Update.Combine(
                Builders<UserEntity>.Update.Set(f => f.Pseudo, pseudo),
                Builders<UserEntity>.Update.Set(f => f.Email, email));

            collection.UpdateOne(filter, update);
        }

        public void ReplaceUserByPassword(string id, string password)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(id);
            var update = Builders<UserEntity>.Update.Set(f => f.HashedPassword, password);

            collection.UpdateOne(filter, update);
        }

        public List<User> MapToList(List<UserEntity> data)
        {
            var result = new List<User>();
            foreach (var item in data)
            {
                result.Add(item.MapToDomain());
            }
            return result;
        }
    }
}
