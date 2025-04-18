﻿using GymProgress.Api.Interface.Map;
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
        [BsonElement("pseudo")]
        public string Pseudo { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("hashedPassword")]
        public string HashedPassword { get; set; }

        public UserEntity(string pseudo, string email, string hashedPassword)
        {
            Pseudo = pseudo;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public User MapToDomain()
        {
            User result = new User
            {
                UserId = Id,
                Pseudo = Pseudo,
                Email = Email,
                HashedPassword = HashedPassword
            };

            return result;
        }
    }
}
