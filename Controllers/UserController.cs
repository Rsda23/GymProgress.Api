using Microsoft.AspNetCore.Mvc;

namespace GymProgress.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService user)
        {
            _service = user;
        }

        [HttpPost("PostUser")]
        public void PostUser(string pseudo, string email, string hashedPassword)
        {
            _service.PostUser(pseudo, email, hashedPassword);
        }

        [HttpGet("GetUserById")]
        public User GetUserById(string id)
        {
            return _service.GetUserById(id);
        }

        [HttpGet("GetUserByPseudo")]
        public User GetUserByPseudo(string pseudo)
        {
            return _service.GetUserByPseudo(pseudo);
        }

        [HttpGet("GetUserByEmail")]
        public User GetUserByEmail(string email)
        {
            return _service.GetUserByEmail(email);
        }

        [HttpDelete("DeleteUserById")]
        public void DeleteUserById(string id)
        {
            _service.DeleteUserById(id);
        }

        [HttpDelete("DeleteUserByEmail")]
        public void DeleteUserByEmail(string email)
        {
            _service.DeleteUserByEmail(email);
        }

        [HttpPut("PutUser")]
        public void PutUser(string id, string pseudo, string email)
        {
            _service.PutUser(id, pseudo, email);
        }

        [HttpPut("PutUserByPassword")]
        public void PutUserByPassword(string id, string password)
        {
            _service.PutUserByPassword(id, password);
        }
    }
}
