using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface IArtistRepo
    {
        public List<Artist> GetAllArtists();
    }
}
