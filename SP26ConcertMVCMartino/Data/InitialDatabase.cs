using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Data
{
    public class InitialDatabase
    {
        public static void InitializeDatabase(IServiceProvider services)
        {
            ApplicationDbContext database = services.GetRequiredService<ApplicationDbContext>();
            UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();

            if (!database.AppUsers.Any())
            {
                AppUser customer = new AppUser("Test.Customer1@test.com", "Test.Customer1", "Ryan", "Martino");
                userManager.CreateAsync(customer).Wait();
            }
            if (!database.Venues.Any())
            {
                Venue venue = new Venue("Venue1", "AddressLine1", null, "Morgantown", "WV", "26000", "USA", VenueType.Indoor, 100);
                database.Venues.Add(venue);
                venue = new Venue("Venue2", "AddressLine2", null, "Tampa", "FL", "27000", "USA", VenueType.Outdoor, 200);
                database.Venues.Add(venue);
                database.SaveChanges();

            }
            if (!database.Events.Any())
            {
                Venue venue = database.Venues.Where(v => v.VenueName == "Venue1").First();
                Event @event = new Event(venue, "Event1", new DateTime(2026, 05, 05), true, 100);
                database.Events.Add(@event);
                @event = new Event(venue, "Event2", new DateTime(2026, 06, 06), true, 0);
                database.Events.Add(@event);

                database.SaveChanges();

            }
            if (database.Tickets.Any())
            {
                AppUser customer = database.AppUsers.Where(a => a.Email == "Test.Customer1@test.com").First();

                Event @event = database.Events.First();

                Ticket ticket = new Ticket(customer, @event, true, 200);

                database.SaveChanges();
            }

        }
            
    }
}
