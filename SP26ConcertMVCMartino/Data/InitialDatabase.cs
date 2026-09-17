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
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (!database.Roles.Any())
            {
                IdentityRole role = new IdentityRole("Customer");
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole("Employee");
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole("Admin");
                roleManager.CreateAsync(role).Wait();
            }

            if (!database.AppUsers.Any())
            {
                AppUser customer = new AppUser("Test.Customer1@test.com", "Test.Customer1", "Ryan", "Martino");
                userManager.CreateAsync(customer).Wait();
                userManager.AddToRoleAsync(customer, "Customer").Wait();


                customer = new AppUser("Test.Employee1@test.com", "Test.Employee1", "John", "Helldiver");
                userManager.CreateAsync(customer).Wait();
                userManager.AddToRoleAsync(customer, "Employee").Wait();


                customer = new AppUser("Test.Admin1@test.com", "Test.Admin1", "Jane", "Smith");
                userManager.CreateAsync(customer).Wait();
                userManager.AddToRolesAsync(customer, new List<string>{ "Admin", "Employee" }).Wait();

            }
            if (!database.Venues.Any())
            {
                Venue venue = new Venue("Venue1", "AddressLine1", null, "Morgantown", "WV", "26000", "USA", VenueType.Indoor, 100);
                database.Venues.Add(venue);
                venue = new Venue("Venue2", "AddressLine2", null, "Tampa", "FL", "27000", "USA", VenueType.Outdoor, 200);
                database.Venues.Add(venue);
                database.SaveChanges();

            }
            if (!database.Artists.Any())
            {
                Artist artist = new Artist("Cody", "Jinks");
                Genre genre = database.Genres.Where(g => g.GenreName == "Country").First();
                artist.Genres.Add(genre);
                database.Artists.Add(artist);

                database.SaveChanges();
                artist = new Artist("Kedrick", "Lemar");
                database.Artists.Add(artist);

                database.SaveChanges();

                artist = new Artist("Colter", "Wall");
                database.Artists.Add(artist);

                database.SaveChanges();
            }
            if (!database.Events.Any())
            {
                Venue venue = database.Venues.Where(v => v.VenueName == "Venue1").First();

                Event @event = new Event(venue, "Event1", new DateTime(2026, 05, 05), true, 100);
                database.Events.Add(@event);
                Artist artist = database.Artists.First();
                @event.Artists.Add(artist);
                @event = new Event(venue, "Event2", new DateTime(2026, 06, 06), true, 0);
                @event.Artists.Add(artist);
                artist = database.Artists.Where(a => a.FullName == "Colter" && a.LastName == "Wall").First();
                @event.Artists.Add(artist);

                database.Events.Add(@event);

                database.SaveChanges();

            }
            
            if (!database.Genres.Any())
            {
                Genre genre = new Genre("Country");
                database.Genres.Add(genre);
                database.SaveChanges();
            }
            if (!database.Tickets.Any())
            {
                AppUser customer = database.AppUsers.Where(a => a.Email == "Test.Customer1@test.com").First();

                Event @event = database.Events.First();

                Ticket ticket = new Ticket(customer, @event, true, 200);

                database.SaveChanges();
            }
            

        }
            
    }
}
