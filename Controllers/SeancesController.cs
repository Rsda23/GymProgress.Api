using Microsoft.AspNetCore.Mvc;

namespace GymProgress.Api.Controllers
{
    public class SeancesController : Controller
    {
        private readonly ISeanceService _service;

        public SeancesController(ISeanceService service)
        {
            _service = service;
        }

        [HttpPost("PostSeance")]
        public void CreateSeance(string name)
        {
            _service.CreateSeance(name);
        }

        [HttpPost("PostSeanceWithExerciceId")]
        public void CreateSeanceWithExerciceId(string nameSeance, List<string> exerciceId)
        {
            _service.CreateSeanceWithExerciceId(nameSeance, exerciceId);
        }

        [HttpPost("PostSeanceWithExerciceName")]
        public void CreateSeanceWithExerciceName(string nameSeance, List<string> exerciceName)
        {
            _service.CreateSeanceWithExerciceName(nameSeance, exerciceName);
        }

        [HttpPost("PostExerciceToSeanceById")]
        public void AddExerciceToSeanceById(string seanceId, List<string> execiceId)
        {
            _service.AddExerciceToSeanceById(seanceId, execiceId);
        }

        [HttpPost("PostExerciceToSeanceByName")]
        public void AddExerciceToSeanceByName(string seanceId, List<string> execiceName)
        {
            _service.AddExerciceToSeanceByName(seanceId, execiceName);
        }

        [HttpGet("GetSeanceById")]
        public Seance GetSeanceById(string id)
        {
            return _service.GetSeanceById(id);
        }

        [HttpGet("GetSeanceByName")]
        public Seance GetSeanceByName(string name)
        {
            return _service.GetSeanceByName(name);
        }

        [HttpDelete("DeleteSeanceById")]
        public void DeleteSeanceById(string id)
        {
            _service.DeleteSeanceById(id);
        }

        [HttpDelete("DeleteSeanceByName")]
        public void DeleteSeanceByName(string name)
        {
            _service.DeleteSeanceByName(name);
        }

        [HttpDelete("DeleteExerciceToSeanceById")]
        public void DeleteExerciceToSeanceById(string Seanceid, List<string> exerciceId)
        {
            _service.DeleteExerciceToSeanceById(Seanceid, exerciceId);
        }

        [HttpDelete("DeleteExerciceToSeanceByName")]
        public void DeleteExerciceToSeanceByName(string Seanceid, List<string> exerciceName)
        {
            _service.DeleteExerciceToSeanceByName(Seanceid, exerciceName);
        }

        [HttpPut("PutSeance")]
        public void ReplaceSeance(string id, string name)
        {
            _service.ReplaceSeance(id, name);
        }

        [HttpPut("PutSeanceExerciceById")]
        public void ReplaceExerciceById(string seanceId, List<string> exerciceId)
        {
            _service.ReplaceExerciceById(seanceId, exerciceId);
        }

        [HttpPut("PutSeanceExerciceByName")]
        public void ReplaceExerciceByName(string seanceId, List<string> exerciceId)
        {
            _service.ReplaceExerciceByName(seanceId, exerciceId);
        }
    }
}