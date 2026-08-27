using Microsoft.Build.Tasks;
using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface IEventRepo
    {
        public List<Event> GetAllEvents();
        public int AddEvent(Event @event);
    }
}
