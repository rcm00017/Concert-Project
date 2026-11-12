using Microsoft.AspNetCore.Mvc;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.ViewModels;

namespace SP26ConcertMVCMartino.Controllers
{
    public class TicketController : Controller
    {
        private IEventRepo iEventRepo;
        private ITicketRepo iTicketRepo;
        private IAppUserRepo iAppUserRepo;

        public TicketController(IEventRepo eventRepo, ITicketRepo ticketRepo, IAppUserRepo appUserRepo)
        {
            this.iEventRepo = eventRepo;
            this.iTicketRepo = ticketRepo;
            this.iAppUserRepo = appUserRepo;
        }
        public IActionResult PurchaseTicketChecks(Event @event)
        {
            //Event is past
            if (@event.DateTime < DateTime.Now)
            {
                ModelState.AddModelError("EventPast", "This event has already occurred.");

                return RedirectToAction("SearchEvents", "Event");
            }
            //Calculate if event is sold out
            if (@event.Tickets.Count >= @event.Venue.Capacity)
            {
                ModelState.AddModelError("EventSoldOut", "This event is sold out.");
                return RedirectToAction("SearchEvents", "Event");
            }
            return null;
        }

        [HttpGet] //Click the BUY button
        public IActionResult ConfirmPurchaseTicket(int eventID)
        {
            Event @event = this.iEventRepo.FindEvent(eventID);

            IActionResult result = PurchaseTicketChecks(@event);
            if (result != null)
            {
                return result;
            }


            //Return a view that show the event details and price
            PurchaseTicketViewModel viewModel = new PurchaseTicketViewModel();
            viewModel.EventID = eventID;
            viewModel.EventName = @event.EventName;
            viewModel.EventDateTime = @event.DateTime.Value;
            viewModel.Price = @event.GeneralAdmissionPrice;
            return View(viewModel);
        }
        [HttpPost] //Click the CONFIRM button
        public IActionResult PurchaseTicket(PurchaseTicketViewModel viewModel)
        {
            Event @event = this.iEventRepo.FindEvent(viewModel.EventID);

            IActionResult result = PurchaseTicketChecks(@event);
            if (result != null)
            {
                return result;
            }

            //Create the new ticket
            AppUser user = this.iAppUserRepo.GetCurrentAppUser();
            Ticket ticket = new Ticket(user, @event, true, viewModel.Price);

            //Save to database
            int ticketID = this.iTicketRepo.AddTicket(ticket);
            //return confirmation with TicketID
            return View();
        }
    }
}
