using Microsoft.EntityFrameworkCore;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;

namespace SP26ConcertMVCMartino.Models
{
    public class EventRepo : IEventRepo
    {
        private ApplicationDbContext database;

        public EventRepo(ApplicationDbContext dbCentext)
        {
            this.database = dbCentext;
        }

        public int AddEvent(Event @event)
        {
            this.database.Events.Add(@event);
            this.database.SaveChanges();

            return @event.EventID;
        }

        public List<Event> GetAllEvents()
        {
            return database.Events.Include(e => e.Artists).ThenInclude(a => a.Genres).Include(e => e.Venue).ToList();
        }
    }
}
