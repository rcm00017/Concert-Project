using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;

namespace SP26ConcertMVCMartino.Models
{
    public class GenreRepo : IGenreRepo
    {
        private ApplicationDbContext database;

        public GenreRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Genre> GetAllGenres()
        {
           return this.database.Genres.ToList();
        }
    }
}
