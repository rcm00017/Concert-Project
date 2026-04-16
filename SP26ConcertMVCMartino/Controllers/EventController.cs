using Microsoft.AspNetCore.Mvc;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.Models.ViewModels;

namespace SP26ConcertMVCMartino.Controllers
{
    public class EventController : Controller
    {
        private IEventRepo iEventRepo;

        public EventController(IEventRepo eventRepo)
        {
            this.iEventRepo = eventRepo;
        }
        [HttpGet]
        public IActionResult SearchEvents()
        {
            //Create a blank form and send to user
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            return View(viewModel);
            return View();
        }
        //Search Button
        [HttpPost]
        //parameters are inputs
        public IActionResult SearchEvents(SearchEventsViewModel viewModel)
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
            events = events.OrderByDescending(e => e.DateTime).ToList();

            //Return results
            viewModel.SearchResult = events; 
            return View(viewModel);
        }
    }
}
