using GymProgress.Api.Interface;
using GymProgress.Api.Models;
using GymProgress.Domain.Models;
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

        [HttpPost("PostFullExercice")]
        public void CreateFullExercice([FromBody] string nom, int repetition, int serie, float charge, string userId)
        {
            _service.CreateFullExercice(nom, repetition, serie, charge, userId);
        }

        [HttpPost("PostExercice")]
        public void CreateExercice([FromBody] string nom)
        {
            _service.CreateExercice(nom);
        }

        [HttpGet("GetAllExercice")]
        public List<Exercice> GetAllExercice()
        {
            return _service.GetAllExercice();
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

        [HttpPut("PutAllExercice")]
        public void ReplaceAllExercice(string id, string name, int repetition, int serie, float charge)
        {
            _service.ReplaceAllExercice(id, name, repetition, serie, charge);
        }

        [HttpPut("PutName")]
        public void UpdateName(string exerciceId, int name)
        {
            _service.UpdateRepetition(exerciceId, name);
        }

        [HttpPut("PutRepetion")]
        public void UpdateRepetition(string exerciceId, int repetition)
        {
            _service.UpdateRepetition(exerciceId, repetition);
        }

        [HttpPut("PutSerie")]
        public void UpdateSerie(string exerciceId, int serie)
        {
            _service.UpdateRepetition(exerciceId, serie);
        }

        [HttpPut("PutCharge")]
        public void UpdateCharge(string exerciceId, float charge)
        {
            _service.UpdateCharge(exerciceId, charge);
        }

    }
}
