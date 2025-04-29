using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface ISetDataService
    {
        public void CreateFullSetData(string exerciceId, int repetition, int serie, float charge, string userId);
        public SetData GetSetDataById(string id);
        public SetData GetSetDataByExerciceId(string exerciceId);
        public void DeleteSetDataById(string id);
        public void UpdateSetData(SetData setData);
        public void UpdateRepetition(string setdataId, int repetition);
        public void UpdateSerie(string setdataId, int serie);
        public void UpdateCharge(string setdataId, float charge);
        public void ReplaceSetData(SetData setData);
    }
}
