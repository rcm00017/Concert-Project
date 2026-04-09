using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [Required]
        public string EventName { get; set; }
        public DateTime? DateTime { get; set; }
        [Required]
        public bool OpenSeatingAvailable { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal GeneralAdmissionPrice { get; set; }


        //BEhind the scenes, VenueID
        // 1-Many the one means it is required
        [Required]
        public Venue Venue { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Event(Venue venue, string eventName, DateTime? dateTime, bool openSeatingAvailable, decimal generalAdmissionPrice)
        {
            this.Venue = venue;
            this.EventName = eventName;
            this.DateTime = dateTime;
            this.OpenSeatingAvailable = openSeatingAvailable;
            this.GeneralAdmissionPrice = generalAdmissionPrice;
        }
        public Event()
        {
            
        }

    }
}
