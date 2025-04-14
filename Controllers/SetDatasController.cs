using GymProgress.Api.Interface;
using GymProgress.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymProgress.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetDatasController : ControllerBase
    {
        private readonly ISetDataService _service;
        public SetDatasController(ISetDataService service)
        {
            _service = service;
        }

        [HttpPost("PostFullSetData")]
        public void CreateFullSetData([FromBody] string exerciceId, int repetition, int serie, float charge)
        {
            _service.CreateFullSetData(exerciceId, repetition, serie, charge);
        }

        [HttpPost("GetSetDataById")]
        public SetData GetSetDataById(string id)
        {
           return _service.GetSetDataById(id);
        }

        [HttpPost("GetSetDataByExerciceId")]
        public SetData GetSetDataByExerciceId(string exerciceId)
        {
            return _service.GetSetDataByExerciceId(exerciceId);
        }

        [HttpPost("DeleteSetDataById")]
        public void DeleteSetDataById(string id)
        {
            _service.DeleteSetDataById(id);
        }

        [HttpPost("UpdateSetData")]
        public void UpdateSetData(string setDataId, int repetition, int serie, float charge)
        {
            _service.UpdateSetData(setDataId, repetition, serie, charge);
        }

        [HttpPost("UpdateRepetition")]
        public void UpdateRepetition(string setdataId, int repetition)
        {
            _service.UpdateRepetition(setdataId, repetition);
        }

        [HttpPost("UpdateSerie")]
        public void UpdateSerie(string setdataId, int serie)
        {
            _service.UpdateSerie(setdataId, serie);
        }

        [HttpPost("UpdateCharge")]
        public void UpdateCharge(string setdataId, float charge)
        {
            _service.UpdateCharge(setdataId, charge);
        }
    }
}
