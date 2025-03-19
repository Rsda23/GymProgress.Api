using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        void CreateExercice(string nom, int repetition, int serie, float charge, string userId);
        public List<Exercice> GetAllExercice();
        public Exercice GetExerciceById(string id);
        public Exercice GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public void DeleteExerciceByName(string name);
        public void ReplaceExercice(string id, string name, int repetition, int serie, float charge);
    }
}
