using GymProgress.Api.Interface.Map;
using GymProgress.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymProgress.Api.Models
{
    public class UserEntity : IMapToDomain<User>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public UserEntity(string pseudo, string email, string hashedPassword)
        {
            Pseudo = pseudo;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public User MapToDomain()
        {
            throw new NotImplementedException();
        }
    }
}
