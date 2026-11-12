using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class SeatPrice
    {
        [Key]
        public int SeatPriceID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public Event Event { get; set; }
        [Required]
        public Seat Seat { get; set; }

        public SeatPrice(Event @event, Seat seat, decimal price)
        {
            this.Event = @event;
            this.Seat = seat;
            this.Price = price;
        }
        public SeatPrice()
        {
            
        }
    }
}
