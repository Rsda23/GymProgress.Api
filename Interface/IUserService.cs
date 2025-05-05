using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IUserService
    {
        public void CreateUser(string pseudo, string email, string hashedPassword);
        public List<User> GetAllUser();
        public User GetUserById(string id);
        public User GetUserByPseudo(string pseudo);
        public User GetUserByEmail(string email);
        public void DeleteUserById(string id);
        public void DeleteUserByEmail(string email);
        public void DeleteAllUser();
        public void ReplaceUser(string id, string pseudo, string email);
        public void ReplaceUserByPassword(string id, string password);
    }
}
