using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;

namespace SP26ConcertMVCMartino.Models
{
    public class ArtistRepo : IArtistRepo
    {
        private ApplicationDbContext database;

        public ArtistRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Artist> GetAllArtists()
        {
           return this.database.Artists.ToList();
        }
    }
}
