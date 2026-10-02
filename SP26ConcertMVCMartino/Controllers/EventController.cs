using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

        //HTTP GET = READ
        //HTTP POST = ADD
        //HTTP PATCH = UPDATE
        //HTTP DELETE = DELETE
        //HTTP PUT = UPDATE
        [HttpGet]
        [Authorize] //must be logged in

        public IActionResult AddEvent(int? VenueID)
        {
            //Present the user with a form
            EventViewModel viewModel = new EventViewModel();
            if (VenueID.HasValue)
            {
                viewModel.VenueID = VenueID.Value;
            }
            CreateDropdowns();
            //User fills out the form    
            return View(viewModel);
        }
        [HttpPost] // Submit Button
        [Authorize] //must be logged in

        public IActionResult AddEvent(EventViewModel viewModel)
        {
            //Input Validation
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            //Venue cannot have 2 events at the same time 

            //Venue cannot have 2 events at the same time with the same name (duplicate)

            List<Event> events = this.iEventRepo.GetAllEvents();
            if (events.Any(e => e.Venue.VenueID == viewModel.VenueID && e.DateTime != null && e.DateTime == viewModel.DateTime))
            {
                //ERROR
                ModelState.AddModelError("Duplicate", "An event at this location and date/time already exists!");
                return View(viewModel);
            }
            //Event should not be in the past
            if (viewModel.DateTime < DateTime.Now)
            {
                ModelState.AddModelError("EventPast", "The date/time of this event is in the past!");
                return View(viewModel);
            }

            //Add Event or new record
            decimal price = -1.0m;
            if (viewModel.GeneralAdmissionPrice != null)
            {
                price = viewModel.GeneralAdmissionPrice.Value;
            }
            Venue venue = this.iVenueRepo.GetAllVenues().Where(v => v.VenueID == viewModel.VenueID).FirstOrDefault();

            Event @event = new Event(venue, viewModel.EventName, viewModel.DateTime, viewModel.OpenSeatingAvailable, price);

            //Save in DB
            int eventID = this.iEventRepo.AddEvent(@event);
            @event.EventName = viewModel.EventName;
            @event.DateTime = viewModel.DateTime;
            @event.GeneralAdmissionPrice = price;
            @event.OpenSeatingAvailable = viewModel.OpenSeatingAvailable;
            @event.Venue = venue;


            //Return a success
            return RedirectToAction("SearchEvents");
        }
        [HttpGet]
        [Authorize(Roles = "Employee")] //must be logged in
        public IActionResult EditEvent(int eventID)
        {
            if (eventID < 1)
            {
                return RedirectToAction("SearchEvents");
            }
            //Present the user with a form
            EventViewModel viewModel = new EventViewModel();
            Event @event = this.iEventRepo.FindEvent(eventID);

            if (@event == null)
            {
                return RedirectToAction("SearchEvents");
            }

            viewModel.EventName = @event.EventName;
            viewModel.DateTime = @event.DateTime;
            viewModel.GeneralAdmissionPrice = @event.GeneralAdmissionPrice;
            viewModel.OpenSeatingAvailable = @event.OpenSeatingAvailable;
            viewModel.VenueID = @event.Venue.VenueID;
            viewModel.EventID = @event.EventID;

            CreateDropdowns();
            //User fills out the form    
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")] //must be logged in

        public IActionResult EditEvent(EventViewModel viewModel)
        {
            //Input Validation
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            //Venue cannot have 2 events at the same time 

            //Venue cannot have 2 events at the same time with the same name (duplicate)

            List<Event> events = this.iEventRepo.GetAllEvents();
            if (events.Any(e => e.EventID != viewModel.EventID && e.Venue.VenueID == viewModel.VenueID && e.DateTime != null && e.DateTime.Value.Year == viewModel.DateTime.Value.Year && e.DateTime.Value.Month == viewModel.DateTime.Value.Month && e.DateTime.Value.Day == viewModel.DateTime.Value.Day && e.DateTime.Value.Hour == viewModel.DateTime.Value.Hour))
            {
                //ERROR
                ModelState.AddModelError("Duplicate", "An event at this location and date/time already exists!");
                return View(viewModel);
            }
            if (viewModel.DateTime <= DateTime.Now)
            {
                ModelState.AddModelError("EventPast", "The date/time of this event is in the past!!");
                return View(viewModel);
            }


            //edit event
            decimal price = -1.0m;
            if (viewModel.GeneralAdmissionPrice != null)
            {
                price = viewModel.GeneralAdmissionPrice.Value;
            }

            Venue venue = this.iVenueRepo.GetAllVenues().Where(v => v.VenueID == viewModel.VenueID).FirstOrDefault();


            //
            Event @event = this.iEventRepo.FindEvent(viewModel.EventID);

            if (@event == null)
            {
                return RedirectToAction("SearchEvents");
            }
            //Event should not be in the past
            if (@event.DateTime <= DateTime.Now)
            {
                ModelState.AddModelError("EventPast", "This event has already taken place!");
                return View(viewModel);
            }
            

            @event.EventName = viewModel.EventName; 
            @event.DateTime = viewModel.DateTime;
            @event.GeneralAdmissionPrice = price;
            @event.OpenSeatingAvailable = viewModel.OpenSeatingAvailable;
            @event.Venue = venue;

            // Save in DB
            this.iEventRepo.EditEvent(@event);
            //Return a success
            return RedirectToAction("SearchEvents");


            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")] //must be logged in
        public IActionResult ConfirmDeleteEvent(int eventID)
        {
            if (eventID < 1)
            {
                return RedirectToAction("SearchEvents");
            }
            //Present the user with a form
            EventViewModel viewModel = new EventViewModel();
            Event @event = this.iEventRepo.FindEvent(eventID);

            if (@event == null)
            {
                return RedirectToAction("SearchEvents");
            }

            viewModel.EventName = @event.EventName;
            viewModel.DateTime = @event.DateTime;
            viewModel.GeneralAdmissionPrice = @event.GeneralAdmissionPrice;
            viewModel.OpenSeatingAvailable = @event.OpenSeatingAvailable;
            viewModel.VenueID = @event.Venue.VenueID;
            viewModel.EventID = @event.EventID;

            CreateDropdowns();
            //User fills out the form    
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")] //must be logged in

        public IActionResult DeleteEvent(EventViewModel viewModel)
        {
            //Input Validation
            if (viewModel.EventID < 1)
            {
                return RedirectToAction("SearchEvents");
            }

            //Find existing event
            Event @event = this.iEventRepo.FindEvent(viewModel.EventID);

            if (@event == null)
            {
                return RedirectToAction("SearchEvents");
            }
            //Event should not be in the past
            if (@event.DateTime <= DateTime.Now)
            {
                ModelState.AddModelError("EventPast", "This event has already taken place!");
                return View("ConfirmDeleteEvent",viewModel);
            }
            //If any tickets sold, cannot delete event
            if(@event.Tickets.Any())
            {
                ModelState.AddModelError("TicketsSold", "Cannot delete an event with tickets sold!");
                return View(viewModel);
            }

            //Delete Record
            this.iEventRepo.DeleteEvent(@event);


            //Return the user somewhere
            return RedirectToAction("SearchEvents");
        }
        public IActionResult ViewEventDetails(int EventID)
        {
            //Retrieve the event from the database
            if(EventID < 1)
            {
                return RedirectToAction("SearchEvents");
            }
            Event @event = this.iEventRepo.FindEvent(EventID);

            if (@event == null)
            {
                return RedirectToAction("SearchEvents");
            }

            //Send the event to the view
            return View(@event);
        }

    }//Class
}//Namespace
