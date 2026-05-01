using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface IGenreRepo
    {
        public List<Genre> GetAllGenres();
    }
}
