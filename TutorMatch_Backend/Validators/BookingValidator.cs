using TutorMatch_Backend.DTOs;

namespace TutorMatch_Backend.Validators
{
    public class BookingValidator
    {
        public static bool Validate(BookingDto booking)
        {
            if (string.IsNullOrWhiteSpace(booking.StudentName))
                return false;

            if (string.IsNullOrWhiteSpace(booking.Subject))
                return false;

            if (booking.TutorId <= 0)
                return false;

            return true;
        }
    }
}
