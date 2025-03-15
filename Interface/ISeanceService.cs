using GymProgress.Api.Models;
using GymProgress.Domain.Models;

namespace GymProgress.Api.Interface
{
    public interface ISeanceService
    {
        public void CreateSeance(string name);
        public void CreateSeanceWithExerciceId(string nameSeance, List<string> exerciceId);
        public void CreateSeanceWithExerciceName(string nameSeance, List<string> exerciceName);
        public void AddExerciceToSeanceById(string seanceId, List<string> execiceId);
        public void AddExerciceToSeanceByName(string seanceId, List<string> execiceName);
        public List<Seance> GetAllSeance();
        public SeanceEntity GetSeanceById(string id);
        public SeanceEntity GetSeanceByName(string name);
        public void DeleteSeanceById(string id);
        public void DeleteSeanceByName(string name);
        public void DeleteExerciceToSeanceById(string Seanceid, List<string> exerciceId);
        public void DeleteExerciceToSeanceByName(string Seanceid, List<string> exerciceName);
        public void ReplaceSeance(string id, string name);
        public void ReplaceExerciceById(string seanceId, List<string> exerciceId);
        public void ReplaceExerciceByName(string seanceId, List<string> exerciceId);
    }
}