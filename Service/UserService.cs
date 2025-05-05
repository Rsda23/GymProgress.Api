using GymProgress.Api.Entities;
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

        public async void DeleteUserById(string id)
        {
            var collectionUser = _database.GetCollection<UserEntity>("users");
            var collectionSetData = _database.GetCollection<SetDataEntity>("setdatas");
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");

            var filterUser = MongoHelper.BuildFindByIdRequest<UserEntity>(id);
            var filterSetData = Builders<SetDataEntity>.Filter.Eq(x => x.UserId, id);
            var filterSeance = Builders<SeanceEntity>.Filter.Eq(x => x.UserId, id);
            var filterExercice = Builders<ExerciceEntity>.Filter.Eq(x => x.UserId, id);

            var setDatasId = collectionSetData.AsQueryable().Where(sd => sd.UserId == id).Select(sd => sd.Id).ToList();

            var updateExercice = Builders<ExerciceEntity>.Update.PullFilter(e => e.SetDatas, sd => setDatasId.Contains(sd.Id));

            await collectionExercice.UpdateManyAsync(Builders<ExerciceEntity>.Filter.Empty, updateExercice);

            collectionUser.DeleteOne(filterUser);
            collectionSetData.DeleteMany(filterSetData);
            collectionSeance.DeleteOne(filterSeance);
            collectionExercice.DeleteOne(filterExercice);
        }

        public async void DeleteAllUser()
        {
            var collectionUser = _database.GetCollection<UserEntity>("users");
            var collectionSetData = _database.GetCollection<SetDataEntity>("setDatas");
            var collectionSeance = _database.GetCollection<SeanceEntity>("seances");
            var collectionExercice = _database.GetCollection<ExerciceEntity>("exercices");

            var users = GetAllUser();
            List<string> usersId = users.Select(e => e.UserId).Where(id => !string.IsNullOrWhiteSpace(id)).ToList();

            var setDatasId = collectionSetData.AsQueryable().Where(sd => usersId.Contains(sd.UserId)).Select(sd => sd.Id).ToList();

            var updateExercice = Builders<ExerciceEntity>.Update.PullFilter(e => e.SetDatas, sd => setDatasId.Contains(sd.Id));
            await collectionExercice.UpdateManyAsync(Builders<ExerciceEntity>.Filter.Empty, updateExercice);

            var filterSetData = Builders<SetDataEntity>.Filter.In(x => x.UserId, usersId);
            var filterSeance = Builders<SeanceEntity>.Filter.In(x => x.UserId, usersId);
            var filterExercice = Builders<ExerciceEntity>.Filter.In(x => x.UserId, usersId);

            await collectionUser.DeleteManyAsync(Builders<UserEntity>.Filter.Empty);
            collectionSetData.DeleteMany(filterSetData);
            collectionSeance.DeleteMany(filterSeance);
            collectionExercice.DeleteMany(filterExercice);
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
