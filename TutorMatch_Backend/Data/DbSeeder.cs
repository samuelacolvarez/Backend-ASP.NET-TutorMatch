using Bogus; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using TutorMatch_Backend.Models;

namespace TutorMatch_Backend.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await db.Database.EnsureCreatedAsync();

            // Roles
            foreach (var role in new[] { "Admin", "Tutor", "Student" })
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            if (db.Users.Any()) return; // already seeded

            var faker = new Faker("es");
            var subjects = new[] { "Cálculo", "Álgebra", "Física", "Programación", "Python", "Química", "Historia", "Inglés" };
            var modalities = new[] { "Online", "InPerson", "Both" };
            var levels = new[] { "Básico", "Intermedio", "Avanzado" };

            // Admin
            var admin = new AppUser { UserName = "admin@tutormatch.com", Email = "admin@tutormatch.com", FullName = "Admin TutorMatch", IsStudent = true };
            await userManager.CreateAsync(admin, "Admin@1234!");
            await userManager.AddToRoleAsync(admin, "Admin");

            // 5 Tutor+Student users
            var tutorUsers = new List<AppUser>();
            for (int i = 0; i < 5; i++)
            {
                var u = new AppUser
                {
                    UserName = faker.Internet.Email(),
                    Email = faker.Internet.Email(),
                    FullName = faker.Name.FullName(),
                    Bio = faker.Lorem.Sentence(),
                    IsTutor = true,
                    IsStudent = true,
                    VisibilityScore = 1.0
                };
                u.UserName = u.Email;
                var res = await userManager.CreateAsync(u, "Tutor@1234!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(u, "Tutor");
                    await userManager.AddToRoleAsync(u, "Student");
                    tutorUsers.Add(u);
                }
            }

            // 5 Student-only users
            var studentUsers = new List<AppUser>();
            for (int i = 0; i < 5; i++)
            {
                var u = new AppUser
                {
                    UserName = faker.Internet.Email(),
                    Email = faker.Internet.Email(),
                    FullName = faker.Name.FullName(),
                    IsStudent = true
                };
                u.UserName = u.Email;
                var res = await userManager.CreateAsync(u, "Student@1234!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(u, "Student");
                    studentUsers.Add(u);
                }
            }

            // TutorProfiles + Offers + AvailabilitySlots
            var profiles = new List<TutorProfile>();
            foreach (var tu in tutorUsers)
            {
                var chosenSubjects = faker.Random.ArrayElements(subjects, 3);
                var profile = new TutorProfile
                {
                    UserId = tu.Id,
                    Subjects = string.Join(",", chosenSubjects),
                    HourlyRate = faker.Random.Decimal(30000, 80000),
                    Modality = faker.Random.ArrayElement(modalities),
                    AverageRating = Math.Round(faker.Random.Double(3.5, 5.0), 1),
                    ReviewCount = faker.Random.Int(5, 50)
                };
                db.TutorProfiles.Add(profile);
                profiles.Add(profile);

                // 6 offers per tutor (30 total)
                for (int o = 0; o < 6; o++)
                {
                    db.TutoringOffers.Add(new TutoringOffer
                    {
                        TutorProfile = profile,
                        Subject = faker.Random.ArrayElement(chosenSubjects),
                        Level = faker.Random.ArrayElement(levels),
                        Description = faker.Lorem.Paragraph(),
                        DurationOptions = "60,90,120"
                    });
                }

                // Availability
                foreach (var day in new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday })
                {
                    db.AvailabilitySlots.Add(new AvailabilitySlot
                    {
                        TutorProfile = profile,
                        DayOfWeek = day,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(18, 0)
                    });
                }
            }

            await db.SaveChangesAsync();

            // 40 Bookings
            var allStudents = studentUsers.Concat(tutorUsers).ToList();
            var statuses = new[] { BookingStatus.Completed, BookingStatus.Accepted, BookingStatus.Pending, BookingStatus.Cancelled, BookingStatus.Rejected };
            var bookings = new List<Booking>();

            for (int i = 0; i < 40; i++)
            {
                var student = faker.Random.ArrayElement(allStudents.ToArray());
                var tutorUser = faker.Random.ArrayElement(tutorUsers.ToArray());
                if (student.Id == tutorUser.Id) continue;

                var status = faker.Random.ArrayElement(statuses);
                var booking = new Booking
                {
                    StudentId = student.Id,
                    TutorId = tutorUser.Id,
                    OfferId = profiles.First(p => p.UserId == tutorUser.Id).Offers.First().Id,
                    ProposedDateTime = DateTime.UtcNow.AddDays(faker.Random.Int(-30, 30)),
                    DurationMinutes = faker.Random.ArrayElement(new[] { 60, 90, 120 }),
                    Message = faker.Lorem.Sentence(),
                    Status = status,
                    ReviewSubmitted = status == BookingStatus.Completed
                };
                db.Bookings.Add(booking);
                bookings.Add(booking);
            }

            await db.SaveChangesAsync();

            // 30 Messages (on accepted bookings)
            var acceptedBookings = bookings.Where(b => b.Status == BookingStatus.Accepted).ToList();
            int msgCount = 0;
            foreach (var booking in acceptedBookings)
            {
                if (msgCount >= 30) break;
                for (int m = 0; m < 3 && msgCount < 30; m++, msgCount++)
                {
                    db.Messages.Add(new Message
                    {
                        BookingId = booking.Id,
                        SenderId = m % 2 == 0 ? booking.StudentId : booking.TutorId,
                        Content = faker.Lorem.Sentence()
                    });
                }
            }

            // 15 Reviews (on completed bookings)
            var completedBookings = bookings.Where(b => b.Status == BookingStatus.Completed).Take(15).ToList();
            foreach (var booking in completedBookings)
            {
                db.Reviews.Add(new Review
                {
                    BookingId = booking.Id,
                    StudentId = booking.StudentId,
                    TutorId = booking.TutorId,
                    Rating = faker.Random.Int(3, 5),
                    Comment = faker.Lorem.Sentence()
                });
            }

            await db.SaveChangesAsync();
        }
    }
}
