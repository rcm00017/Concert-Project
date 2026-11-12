using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Row
    {
        [Key]
        public int RowID { get; set; }
        [Required]
        public string RowName { get; set; }

        [Required]
        public Section Section { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();

        public Row(Section section, string name)
        {
            Section = section;
            RowName = name;
        }
        public Row()
        {
            
        }
    }
}
