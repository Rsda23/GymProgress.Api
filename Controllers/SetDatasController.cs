using GymProgress.Api.Interface;
using GymProgress.Domain.Models;
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
        public void CreateFullSetData([FromBody] SetData setData)
        {
            _service.CreateFullSetData(setData.ExerciceId, setData.Repetition, setData.Serie, setData.Charge, setData.UserId);
        }

        [HttpGet("GetSetDataById")]
        public SetData GetSetDataById(string id)
        {
           return _service.GetSetDataById(id);
        }

        [HttpGet("GetSetDataByExerciceId")]
        public SetData GetSetDataByExerciceId(string exerciceId)
        {
            return _service.GetSetDataByExerciceId(exerciceId);
        }

        [HttpGet("GetSetDataByUserId")]
        public List<SetData> GetSetDataByUserId(string userId)
        {
            return _service.GetSetDataByUserId(userId);
        }

        [HttpGet("GetSetDataByExerciceAndUser")]
        public List<SetData> GetSetDataByExerciceAndUser([FromQuery] string exerciceId, [FromQuery] string userId)
        {
            return _service.GetSetDataByExerciceAndUser(exerciceId, userId);
        }

        [HttpDelete("DeleteSetDataById")]
        public void DeleteSetDataById(string id)
        {
            _service.DeleteSetDataById(id);
        }

        [HttpPut("UpdateSetData")]
        public void UpdateSetData([FromBody] SetData setData)
        {
            _service.UpdateSetData(setData);
        }

        [HttpPut("UpdateRepetition")]
        public void UpdateRepetition(string setdataId, int repetition)
        {
            _service.UpdateRepetition(setdataId, repetition);
        }

        [HttpPut("UpdateSerie")]
        public void UpdateSerie(string setdataId, int serie)
        {
            _service.UpdateSerie(setdataId, serie);
        }

        [HttpPut("UpdateCharge")]
        public void UpdateCharge(string setdataId, float charge)
        {
            _service.UpdateCharge(setdataId, charge);
        }

        [HttpPut("ReplaceSetData")]
        public void ReplaceSetData([FromBody] SetData setData)
        {
            _service.ReplaceSetData(setData);
        }
    }
}
