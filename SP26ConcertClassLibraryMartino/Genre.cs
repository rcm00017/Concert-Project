using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        [Required]
        public string GenreName { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();

        public Genre(string Name)
        {
            this.GenreName = Name;
        }
        public Genre()
        {
            
        }
    }
    
    
}
