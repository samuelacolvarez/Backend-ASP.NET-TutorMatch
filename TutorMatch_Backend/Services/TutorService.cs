using TutorMatch_Backend.Models;

namespace TutorMatch_Backend.Services
{
    public class TutorService
    {
        private static List<Tutor> tutors = new();

        public List<Tutor> GetAll()
        {
            return tutors;
        }

        public Tutor? GetById(int id)
        {
            return tutors.FirstOrDefault(t => t.Id == id);
        }

        public Tutor Add(Tutor tutor)
        {
            tutor.Id = tutors.Count + 1;

            tutors.Add(tutor);

            return tutor;
        }

        public bool Delete(int id)
        {
            var tutor = GetById(id);

            if (tutor == null)
                return false;

            tutors.Remove(tutor);

            return true;
        }
    }
}