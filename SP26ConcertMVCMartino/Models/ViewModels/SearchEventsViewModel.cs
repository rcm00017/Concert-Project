using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models.ViewModels
{
    public class SearchEventsViewModel
    {
        //declare inputs

        public DateTime? DateTime { get; set; }

        //Result
        public List<Event> SearchResult { get; set; } = new List<Event>();

    }
}
