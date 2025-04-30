using GymProgress.Api.Interface;
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

        [HttpPost("PostExercice")]
        public void CreateExercice([FromBody] Exercice exercice)
        {
            _service.CreateExercice(exercice.Nom, exercice.UserId);
        }

        [HttpPost("PostSetDataToExerciceById")]
        public void AddSetDataById(string exerciceId, List<string> setDataId)
        {
            _service.AddSetDataById(exerciceId, setDataId);
        }

        [HttpGet("GetAllExercice")]
        public List<Exercice> GetAllExercice()
        {
            return _service.GetAllExercice();
        }

        [HttpGet("GetExerciceById")]
        public Exercice GetExerciceById([FromQuery] string id)
        {
            return _service.GetExerciceById(id);
                
        }

        [HttpGet("GetExerciceByName")]
        public Exercice GetExerciceByName([FromQuery] string name)
        {
            return _service.GetExerciceByName(name);
        }
        [HttpGet("GetExercicePublic")]
        public List<Exercice> GetExercicePublic()
        {
            return _service.GetExercicePublic();
        }
        [HttpGet("GetExerciceUserId")]
        public List<Exercice> GetExerciceUserId([FromQuery] string userId)
        {
            return _service.GetExerciceUserId(userId);
        }

        [HttpDelete("DeleteExerciceById")]
        public void DeleteExerciceById(string id)
        {
            _service.DeleteExerciceById(id);
        }

        [HttpPut("PutName")]
        public void UpdateName(string exerciceId, string name)
        {
            _service.UpdateName(exerciceId, name);
        }
    }
}
