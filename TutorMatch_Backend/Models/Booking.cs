namespace TutorMatch_Backend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public AppUser? Student { get; set; }
        public AppUser? Tutor { get; set; }
    }
}