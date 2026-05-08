namespace TutorMatch_Backend.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public int TutorId { get; set; }
    }
}
