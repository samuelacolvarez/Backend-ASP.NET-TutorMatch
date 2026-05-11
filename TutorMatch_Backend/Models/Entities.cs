using Microsoft.AspNetCore.Identity;

namespace TutorMatch_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public bool IsTutor { get; set; }
        public bool IsStudent { get; set; }
        public int CancellationCount { get; set; }
        public double VisibilityScore { get; set; } = 1.0;

        public TutorProfile? TutorProfile { get; set; }
        public ICollection<Booking> BookingsAsStudent { get; set; } = [];
        public ICollection<Booking> BookingsAsTutor { get; set; } = [];
        public ICollection<Review> ReviewsGiven { get; set; } = [];
        public ICollection<Review> ReviewsReceived { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
        public ICollection<Report> ReportsSubmitted { get; set; } = [];
    }

    public class TutorProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Subjects { get; set; } = string.Empty; // CSV
        public decimal HourlyRate { get; set; }
        public string Modality { get; set; } = "Online"; // Online | InPerson | Both
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public AppUser User { get; set; } = null!;
        public ICollection<TutoringOffer> Offers { get; set; } = [];
        public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = [];
    }

    public class TutoringOffer
    {
        public int Id { get; set; }
        public int TutorProfileId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DurationOptions { get; set; } = string.Empty; // CSV e.g. "60,90,120"
        public bool IsActive { get; set; } = true;

        public TutorProfile TutorProfile { get; set; } = null!;
    }

    public class AvailabilitySlot
    {
        public int Id { get; set; }
        public int TutorProfileId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public TutorProfile TutorProfile { get; set; } = null!;
    }

    public class Booking
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
        public int OfferId { get; set; }
        public DateTime ProposedDateTime { get; set; }
        public int DurationMinutes { get; set; }
        public string? Message { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AcceptDeadline { get; set; }
        public bool ReviewSubmitted { get; set; }

        public AppUser Student { get; set; } = null!;
        public AppUser Tutor { get; set; } = null!;
        public TutoringOffer Offer { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = [];
        public Review? Review { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Accepted,
        Rejected,
        Completed,
        Cancelled
    }

    public class Message
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Booking Booking { get; set; } = null!;
        public AppUser Sender { get; set; } = null!;
    }

    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsReported { get; set; }

        public Booking Booking { get; set; } = null!;
        public AppUser Student { get; set; } = null!;
        public AppUser Tutor { get; set; } = null!;
    }

    public class Report
    {
        public int Id { get; set; }
        public string ReporterId { get; set; } = string.Empty;
        public string? ReportedUserId { get; set; }
        public int? ReportedReviewId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool IsResolved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public AppUser Reporter { get; set; } = null!;
    }
}