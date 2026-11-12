using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public bool Paid { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        [Required]
        public AppUser AppUser { get; set; }
        [Required]
        public Event Event { get; set; }

        public Seat? Seat { get; set; }

        public Ticket(AppUser user, Event @event, bool paid, decimal cost, Seat? seat = null)
        {
            this.AppUser = user;
            this.Event = @event;
            this.Paid = paid;
            this.Cost = cost;
            Seat = seat;
        }
        public Ticket()
        {
            
        }



    }
}
