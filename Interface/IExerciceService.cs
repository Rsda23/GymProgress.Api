using GymProgress.Api.Entities;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        public void CreateExercice(string nom, string userId);
        public void AddSetDataById(string exerciceId, List<string> setDataId);
        public List<Exercice> GetAllExercice();
        public Exercice GetExerciceById(string id);
        public Exercice GetExerciceByName(string name);
        public Exercice GetExerciceByNameAndUser(string name, string userId);
        public List<Exercice> GetExercicePublic();
        public List<Exercice> GetExerciceUserId(string userId);
        public Task<List<Exercice>> GetExercicesBySeanceId(string seanceId);
        public void DeleteExerciceById(string id);
        public void DeleteAllExercice();
        public Task AddSetToExercice(string exerciceId, SetDataEntity setData);
        public void UpdateName(string exerciceId, string name);
        public Task ReplaceSetToExercice(string exerciceId, SetDataEntity setData);
    }
}
