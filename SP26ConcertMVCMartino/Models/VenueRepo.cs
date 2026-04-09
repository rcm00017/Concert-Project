using Microsoft.EntityFrameworkCore;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;

namespace SP26ConcertMVCMartino.Models
{
    public class VenueRepo : IVenueRepo
    {
        private ApplicationDbContext database;
        //Dependency Injection
        public VenueRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Venue> GetAllVenues()
        {

            //Get all Venues
            return database.Venues.Include(v => v.Events).ToList();
            //return
        }
    }
}
