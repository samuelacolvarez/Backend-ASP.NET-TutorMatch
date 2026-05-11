using TutorMatch_Backend.Data;
using TutorMatch_Backend.Models;

namespace TutorMatch_Backend.Services
{
    public class TutorService(AppDbContext context)
    {
        public List<Tutor> GetAll()
        {
            return context.Tutors.ToList();
        }

        public Tutor? GetById(int id)
        {
            return context.Tutors.FirstOrDefault(t => t.Id == id);
        }

        public Tutor Add(Tutor tutor)
        {
            context.Tutors.Add(tutor);
            context.SaveChanges();
            return tutor;
        }

        public bool Delete(int id)
        {
            var tutor = GetById(id);
            if (tutor == null) return false;

            context.Tutors.Remove(tutor);
            context.SaveChanges();
            return true;
        }
    }
}