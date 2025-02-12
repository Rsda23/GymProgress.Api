namespace GymProgress.Api
{
    public interface IExerciceService
    {
        void PostExercice(string nom, int repetition, int serie, float charge, string userId);
        Exercice GetExerciceById(string id);
        Exercice GetExerciceByName(string name);
        public void DeleteExerciceById(string id);
        public void DeleteExerciceByName(string name);
    }
}
