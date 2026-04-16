using SP26ConcertClassLibraryMartino;
using System.ComponentModel.DataAnnotations;

namespace SP26ConcertMVCMartino.Models.ViewModels
{
    public class SearchEventsViewModel
    {
        //declare inputs
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }
        public string? EventName { get; set; }

        public int? ArtistID { get; set; }
        public int? GenreID { get; set; }
        public int? VenueID { get; set; }

        //Result
        public List<Event> SearchResult { get; set; } = new List<Event>();

    }
}
