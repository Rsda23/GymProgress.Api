namespace GymProgress.Api
{
    public interface IUserService
    {
        public void PostUser(string pseudo, string email, string hashedPassword);
        public User GetUserById(string id);
        public User GetUserByPseudo(string pseudo);
        public User GetUserByEmail(string email);
        public void DeleteUserById(string id);
        public void DeleteUserByEmail(string email);
        public void PutUser(string id, string pseudo, string email);
        public void PutUserByPassword(string id, string password);
    }
}
