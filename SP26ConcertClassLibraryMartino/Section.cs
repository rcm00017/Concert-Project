using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }
        [Required]
        public string SectionName { get; set; }
        [Required]
        public Venue Venue { get; set; }
        public List<Row> Rows { get; set; } = new List<Row>();

        public Section(Venue venue, string name )
        {
            this.Venue = venue;
            this.SectionName = name;
            
        }
        public Section()
        {
            
        }
    }
}
