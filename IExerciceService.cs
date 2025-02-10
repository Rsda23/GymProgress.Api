namespace GymProgress.Api
{
    public interface IExerciceService
    {
        void AddExercice(string nom, int repetition, int serie, float charge, DateTime date);
        Exercice GetExerciceById(string id);
        Exercice GetExerciceByName(string name);
    }
}
