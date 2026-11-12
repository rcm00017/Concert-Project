using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;
using System.Security.Claims;

namespace SP26ConcertMVCMartino.Models
{
    public class AppUserRepo : IAppUserRepo
    {
        private IHttpContextAccessor httpContextAccessor;
        private ApplicationDbContext database;
        public AppUserRepo(IHttpContextAccessor accessor, ApplicationDbContext dbContext)
        {
            this.httpContextAccessor = accessor;
            this.database = dbContext;
        }
        public AppUser GetCurrentAppUser()
        {
            string id = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return this.database.AppUsers.Find(id);
        }
    }
}
