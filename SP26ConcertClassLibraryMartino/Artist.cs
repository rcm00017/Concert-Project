using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public List<Event> Events { get; set; } = new List<Event>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public Artist(string first, string last)
        {
            this.FirstName = first;
            this.LastName = last;
        }
        public Artist()
        {
            
        }
    }
}
