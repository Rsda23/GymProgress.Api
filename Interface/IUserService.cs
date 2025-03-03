using GymProgress.Api.Models;

namespace GymProgress.Api.Interface
{
    public interface IUserService
    {
        public void CreateUser(string pseudo, string email, string hashedPassword);
        public List<UserEntity> GetAllUser();
        public UserEntity GetUserById(string id);
        public UserEntity GetUserByPseudo(string pseudo);
        public UserEntity GetUserByEmail(string email);
        public void DeleteUserById(string id);
        public void DeleteUserByEmail(string email);
        public void ReplaceUser(string id, string pseudo, string email);
        public void ReplaceUserByPassword(string id, string password);
    }
}
