using SP26ConcertClassLibraryMartino;
using System.ComponentModel.DataAnnotations;

namespace SP26ConcertMVCMartino.ViewModels
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        [Required]
        public string EventName { get; set; }
        public DateTime? DateTime { get; set; }
        [Required]
        public bool OpenSeatingAvailable { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal? GeneralAdmissionPrice { get; set; }


        //BEhind the scenes, VenueID
        // 1-Many the one means it is required
        [Required]
        public int VenueID { get; set; }
    }
}
