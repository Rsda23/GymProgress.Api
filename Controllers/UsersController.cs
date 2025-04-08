using GymProgress.Api.Interface;
using GymProgress.Api.Models;
using GymProgress.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymProgress.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService user)
        {
            _service = user;
        }

        [HttpPost("PostUser")]
        public void CreateUser([FromBody] User user)
        {
            _service.CreateUser(user.Pseudo, user.Email, user.HashedPassword);
        }

        [HttpGet("GetAllUser")]
        public List<User> GetAllUser()
        {
            return _service.GetAllUser();
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
        public void ReplaceUser(string id, string pseudo, string email)
        {
            _service.ReplaceUser(id, pseudo, email);
        }

        [HttpPut("PutUserByPassword")]
        public void ReplaceUserByPassword(string id, string password)
        {
            _service.ReplaceUserByPassword(id, password);
        }
    }
}
