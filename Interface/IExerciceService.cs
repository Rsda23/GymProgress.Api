using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        public void CreateFullExercice(string nom, int repetition, int serie, float charge, string userId);
        public void CreateExercice(string nom);
        public List<Exercice> GetAllExercice();
        public Exercice GetExerciceById(string id);
        public Exercice GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public void DeleteExerciceByName(string name);
        public void ReplaceAllExercice(string id, string name, int repetition, int serie, float charge);
        public void UpdateName(string exerciceId, int name);
        public void UpdateRepetition(string exerciceId, int repetition);
        public void UpdateSerie(string exerciceId, int serie);
        public void UpdateCharge(string exerciceId, float charge);

    }
}
