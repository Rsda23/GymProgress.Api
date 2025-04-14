using GymProgress.Api.Entities;
using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        public void CreateExercice(string nom);
        public void AddSetDataById(string exerciceId, List<string> setDataId);
        public List<Exercice> GetAllExercice();
        public Exercice GetExerciceById(string id);
        public Exercice GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public Task AddSetToExercice(string exerciceId, SetDataEntity setData);
        public void UpdateName(string exerciceId, string name);
    }
}
