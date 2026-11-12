using SP26ConcertClassLibraryMartino;

namespace SP26ConcertMVCMartino.Models
{
    public interface ITicketRepo
    {
        public int AddTicket(Ticket ticket);
    }
}
