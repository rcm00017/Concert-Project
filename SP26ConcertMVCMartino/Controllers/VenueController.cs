using Microsoft.AspNetCore.Mvc;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Models;

namespace SP26ConcertMVCMartino.Controllers
{
    public class VenueController : Controller
    {
        private IVenueRepo ivenueRepo;

        //Dependency Injection
        //Dependency Inversion
        public VenueController(IVenueRepo venueRepo)
        {
            this.ivenueRepo = venueRepo;
        }
        public IActionResult ListAllVenues()

        {
            //Connect to the Databaase - not directly connect to the DB
            List<Venue> venues= this.ivenueRepo.GetAllVenues();

           
 
            return View(venues);
        }

        
    }
}
