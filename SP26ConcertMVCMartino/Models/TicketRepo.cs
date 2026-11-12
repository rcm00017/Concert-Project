using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Data;
using System.CodeDom;

namespace SP26ConcertMVCMartino.Models
{
    public class TicketRepo : ITicketRepo
    {
        private ApplicationDbContext database;
        public TicketRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public int AddTicket(Ticket ticket)
        {
            this.database.Tickets.Add(ticket);
            this.database.SaveChanges();
            return ticket.TicketID;
        }

    }
}
