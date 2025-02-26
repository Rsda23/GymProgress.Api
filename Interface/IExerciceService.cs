using GymProgress.Api.Models;

namespace GymProgress.Api.Interface
{
    public interface IExerciceService
    {
        void CreateExercice(string nom, int repetition, int serie, float charge, string userId);
        ExerciceEntity GetExerciceById(string id);
        ExerciceEntity GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public void DeleteExerciceByName(string name);
        public void ReplaceExercice(string id, string name, int repetition, int serie, float charge);
    }
}
