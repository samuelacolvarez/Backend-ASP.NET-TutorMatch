using TutorMatch_Backend.DTOs;

namespace TutorMatch_Backend.Validators
{
    public class TutorValidator
    {
        public static bool Validate(TutorDto tutor)
        {
            if (string.IsNullOrEmpty(tutor.Name))
                return false;

            if (string.IsNullOrEmpty(tutor.Subject))
                return false;

            if (string.IsNullOrEmpty(tutor.Email))
                return false;

            return true;
        }
    }
}