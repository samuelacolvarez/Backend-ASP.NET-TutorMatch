namespace TutorMatch_Backend.DTOs
{
    public class BookingDto
    {
        public string StudentName { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public int TutorId { get; set; }
    }
}