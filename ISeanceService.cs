namespace GymProgress.Api
{
    public interface ISeanceService
    {
        public void PostSeance(string name);
        public void PostSeanceWithExerciceId(string name, List<string> exerciceId);
        public Seance GetSeanceById(string id);
        public Seance GetSeanceByName(string name);
        public void DeleteSeanceById(string id);
        public void DeleteSeanceByName(string name);
        public void PutSeance(string id, string name, List<Exercice> exercices);
        public void PutSeanceName(string id, string name);
        public void PutSeanceExercice(string id, List<Exercice> exercices);
    }
}