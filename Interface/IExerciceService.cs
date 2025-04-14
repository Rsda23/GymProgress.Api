using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        public void CreateExercice(string nom);
        public List<Exercice> GetAllExercice();
        public Exercice GetExerciceById(string id);
        public Exercice GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public void DeleteExerciceByName(string name);
        public void UpdateName(string exerciceId, string name);
    }
}
