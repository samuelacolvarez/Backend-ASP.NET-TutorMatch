using Microsoft.EntityFrameworkCore;
using TutorMatch_Backend.Data;
using TutorMatch_Backend.Models;

namespace TutorMatch_Backend.Services
{
    public class BookingService(AppDbContext context)
    {
        public List<Booking> GetAll()
        {
            return context.Bookings.ToList();
        }

        public Booking? GetById(int id)
        {
            return context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public Booking Add(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
            return booking;
        }

        public bool Delete(int id)
        {
            var booking = GetById(id);
            if (booking == null) return false;

            context.Bookings.Remove(booking);
            context.SaveChanges();
            return true;
        }
    }
}