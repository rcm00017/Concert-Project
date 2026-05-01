using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.ViewModels;

namespace SP26ConcertMVCMartino.Controllers
{
    public class EventController : Controller
    {
        private IEventRepo iEventRepo;
        private IArtistRepo iArtistRepo;
        private IVenueRepo iVenueRepo;
        private IGenreRepo iGenreRepo;


        public EventController(IEventRepo eventRepo, IArtistRepo artistRepo, IVenueRepo venueRepo, IGenreRepo iGenreRepo)
        {
            this.iEventRepo = eventRepo;
            this.iArtistRepo = artistRepo;
            this.iVenueRepo = venueRepo;
            this.iGenreRepo = iGenreRepo;
        }

        public void CreateDropdowns()
        {
            ViewData["ArtistDropdown"] = new SelectList(this.iArtistRepo.GetAllArtists(), "ArtistID", "FullName");
            ViewData["VenueDropdown"] = new SelectList(this.iVenueRepo.GetAllVenues(), "VenueID", "VenueName");
            ViewData["GenreDropdown"] = new SelectList(this.iGenreRepo.GetAllGenres(), "GenreID", "GenreName");


        }
        [HttpGet]
        public IActionResult SearchEvents()
        {
            //Create a blank form and send to user
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            //populate dropdown of artists

            CreateDropdowns();


            return View(viewModel);
            
        }
        //Search Button
        //[HttpPost]
        //parameters are inputs
        public IActionResult SearchEventsResult(SearchEventsViewModel viewModel)
        {

            //collect inputs
            List<Event> events = this.iEventRepo.GetAllEvents();

            //Filter events that match inputs
            if (viewModel.DateTime != null)
            {
                events = events.Where(e => e.DateTime != null).ToList();
                events = events.Where(e => e.DateTime.Value.Date == viewModel.DateTime.Value.Date).ToList();
            }

            if (!String.IsNullOrEmpty(viewModel.EventName))
            {
               events = events.Where(e => e.EventName.ToLower().Contains(viewModel.EventName.ToLower().Trim())).ToList();
            }

            if (viewModel.ArtistID != null)
            {
                events = events.Where(e => e.Artists.Any(a => a.ArtistID == viewModel.ArtistID)).ToList();
            }
            if (viewModel.GenreID != null)
            {
                events = events.Where(e => e.Artists.Any(a => a.Genres.Any(g => g.GenreID == viewModel.GenreID))).ToList();
            }
            if (viewModel.VenueID != null)
            {
                events = events.Where(e => e.Venue.VenueID == viewModel.VenueID).ToList();
            }

            //Sort by closest event
            events = events.OrderBy(e => e.DateTime).ToList();

            //Return results
            viewModel.SearchResult = events; 
            return View("SearchEvents", viewModel);
        }
    }
}
