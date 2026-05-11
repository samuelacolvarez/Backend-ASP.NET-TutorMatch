using TutorMatch_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TutorMatch_Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
   public DbSet<TutoringOffer> TutoringOffers => Set<TutoringOffer>();
    public DbSet<AvailabilitySlot> AvailabilitySlots => Set<AvailabilitySlot>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Tutor> Tutors => Set<Tutor>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .Property(u => u.VisibilityScore).HasColumnType("float");


        builder.Entity<Booking>()
            .HasOne(b => b.Student)
            .WithMany(u => u.BookingsAsStudent)
            .HasForeignKey(b => b.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Booking>()
            .HasOne(b => b.Tutor)
            .WithMany(u => u.BookingsAsTutor)
            .HasForeignKey(b => b.TutorId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<Review>()
            .HasOne(r => r.Student)
            .WithMany(u => u.ReviewsGiven)
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Review>()
            .HasOne(r => r.Tutor)
            .WithMany(u => u.ReviewsReceived)
            .HasForeignKey(r => r.TutorId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}

