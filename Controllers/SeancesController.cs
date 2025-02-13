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
        public void PostSeance(string name)
        {
            _service.PostSeance(name);
        }

        [HttpPost("PostSeanceWithExerciceId")]
        public void PostSeanceWithExerciceId(string name, List<string> exerciceId)
        {
            _service.PostSeanceWithExerciceId(name, exerciceId);
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

        [HttpPut("PutSeance")]
        public void PutSeance(string id, string name, List<Exercice> exercices)
        {
            _service.PutSeance(id, name, exercices);
        }

        [HttpPut("PutSeanceName")]
        public void PutSeanceName(string id, string name)
        {
            _service.PutSeanceName(id, name);
        }

        [HttpPut("PutSeanceExercice")]
        public void PutSeanceExercice(string id, List<Exercice> exercices)
        {
            _service.PutSeanceExercice(id, exercices);
        }
    }
}
