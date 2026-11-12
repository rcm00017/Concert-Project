using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }
        [Required]
        public string SeatName { get; set; }

        [Required]
        public Row Row { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public List<SeatPrice> SeatPrices { get; set; } = new List<SeatPrice>();

        public Seat(Row row, string name)
        {
            this.Row = row;
            this.SeatName = name;
        }
        public Seat()
        {
            
        }
    }
}
