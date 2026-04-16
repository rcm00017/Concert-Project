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
        public List<Event> GetAllEvents()
        {
           return database.Events.ToList();
        }
    }
}
