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

            if (string.IsNullOrWhiteSpace(booking.TutorId))
                return false;

            return true;
        }
    }
}
