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
        public void PostExercice(string nom, int repetition, int serie, float charge, string userId)
        {
            _service.PostExercice(nom, repetition, serie, charge, userId);
        }

        [HttpGet("GetExerciceById")]
        public Exercice GetExerciceById(string id)
        {
            return _service.GetExerciceById(id);
        }

        [HttpGet("GetExerciceByName")]
        public Exercice GetExerciceByName(string name)
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

        [HttpPut("PutExerciceByName")]
        public void PutExercice(string id, string name, int repetition, int serie, float charge)
        {
            _service.PutExercice(id, name, repetition, serie, charge);
        }

    }
}
