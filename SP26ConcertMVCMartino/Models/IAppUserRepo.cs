using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface IAppUserRepo
    {
        public AppUser GetCurrentAppUser();
    }
}
