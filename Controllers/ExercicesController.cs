using GymProgress.Api.Interface;
using GymProgress.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymProgress.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercicesController : ControllerBase
    {
        private readonly IExerciceService _service;
        private readonly ILogger<ExercicesController> _logger;


        public ExercicesController(IExerciceService service, ILogger<ExercicesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("PostExercice")]
        public void CreateExercice(string nom, int repetition, int serie, float charge, string userId)
        {
            _service.CreateExercice(nom, repetition, serie, charge, userId);
        }

        [HttpGet("GetAllExercice")]
        public List<ExerciceEntity> GetAllExercice()
        {
            return _service.GetAllExercice();
        }

        [HttpGet("GetExerciceById")]
        public ExerciceEntity GetExerciceById(string id)
        {
            return _service.GetExerciceById(id);
                
        }

        [HttpGet("GetExerciceByName")]
        public ExerciceEntity GetExerciceByName(string name)
        {
            return _service.GetExerciceByName(name);
        }

        [HttpDelete("DeleteExerciceById")]
        public void DeleteExerciceById(string id)
        {
            _service.DeleteExerciceById(id);
        }

        [HttpDelete("DeleteExerciceByName")]
        public void DeleteExerciceByName(string name)
        {
            _service.DeleteExerciceByName(name);
        }

        [HttpPut("PutExercice")]
        public void ReplaceExercice(string id, string name, int repetition, int serie, float charge)
        {
            _service.ReplaceExercice(id, name, repetition, serie, charge);
        }

    }
}
