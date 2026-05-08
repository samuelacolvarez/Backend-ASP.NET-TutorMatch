using TutorMatch_Backend.Models;

namespace TutorMatch_Backend.Services
{
    public class BookingService
    {
        private static List<Booking> bookings = new();

        public List<Booking> GetAll()
        {
            return bookings;
        }

        public Booking? GetById(int id)
        {
            return bookings.FirstOrDefault(b => b.Id == id);
        }

        public Booking Add(Booking booking)
        {
            booking.Id = bookings.Count + 1;

            bookings.Add(booking);

            return booking;
        }

        public bool Delete(int id)
        {
            var booking = GetById(id);

            if (booking == null)
                return false;

            bookings.Remove(booking);

            return true;
        }
    }
}