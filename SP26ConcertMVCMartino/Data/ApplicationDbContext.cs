using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatPrice> SeatPrices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
