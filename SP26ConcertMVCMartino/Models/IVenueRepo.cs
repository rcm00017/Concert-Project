using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface IVenueRepo
    {
        public List<Venue> GetAllVenues();
    }
}
