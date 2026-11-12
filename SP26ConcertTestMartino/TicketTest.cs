using Moq;
using NuGet.ContentModel;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Controllers;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertTestMartino
{
    public class TicketTest
    {
        private Mock<IEventRepo> mockEventRepo { get; set; }
        private Mock<ITicketRepo> mockTicketRepo { get; set; }
        private Mock<IAppUserRepo> mockAppUserRepo { get; set; }
        private TicketController controller { get; set; }

        public TicketTest()
        {
            this.mockEventRepo = new Mock<IEventRepo>();
            this.mockTicketRepo = new Mock<ITicketRepo>();
            this.mockAppUserRepo = new Mock<IAppUserRepo>();
            this.controller = new TicketController(this.mockEventRepo.Object, this.mockTicketRepo.Object, this.mockAppUserRepo.Object);
        }


        [Fact]
        public void ShouldPurchaseTicket()
        {
            // Arrange
            PurchaseTicketViewModel viewModel = new PurchaseTicketViewModel()
            {
                EventID = 1,
                EventName = "Concert",
                EventDateTime = DateTime.Now.AddDays(1),
                Price = 50.00m
            };
            Ticket newTicket = null;
            this.mockTicketRepo.Setup(x => x.AddTicket(It.IsAny<Ticket>())).Callback<Ticket>(t => newTicket = t);

            
            Event @event = new Event()
            {
                EventID = viewModel.EventID,
                EventName = viewModel.EventName,
                DateTime = viewModel.EventDateTime,
                GeneralAdmissionPrice = 100.00m,
                OpenSeatingAvailable = true,
                Venue = new Venue()
                {
                    Capacity = 100
                }
            };
            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(@event);

            AppUser appUser = new AppUser("Test@test.com", "test123", "Test", "User");
            appUser.Id = "1";
            this.mockAppUserRepo.Setup(m => m.GetCurrentAppUser()).Returns(appUser);

            //Act
            this.controller.PurchaseTicket(viewModel);
            //Assert
            this.mockTicketRepo.Verify(x => x.AddTicket(It.IsAny<Ticket>()),  Times.Once);
            Assert.Equal(viewModel.EventID, newTicket.Event.EventID);
            Assert.Equal(viewModel.Price, newTicket.Cost);
            Assert.True(newTicket.Paid);
            Assert.Equal(appUser.Id, newTicket.AppUser.Id);
            Assert.Equal(appUser.Email, newTicket.AppUser.Email);
        }
        [Fact]
        public void ShouldNotPurchaseTicketEventPast()
        {
            // Arrange
            PurchaseTicketViewModel viewModel = new PurchaseTicketViewModel()
            {
                EventID = 1,
                EventName = "Concert",
                EventDateTime = new DateTime(2026,01,01),
                Price = 50.00m
            };
            Ticket newTicket = null;
            this.mockTicketRepo.Setup(x => x.AddTicket(It.IsAny<Ticket>())).Callback<Ticket>(t => newTicket = t);


            Event @event = new Event()
            {
                EventID = viewModel.EventID,
                EventName = viewModel.EventName,
                DateTime = viewModel.EventDateTime,
                GeneralAdmissionPrice = 100.00m,
                OpenSeatingAvailable = true,
                Venue = new Venue()
                {
                    Capacity = 100
                }
            };

            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(@event);

            //Act
            this.controller.PurchaseTicket(viewModel);
            //Assert
            this.mockTicketRepo.Verify(x => x.AddTicket(It.IsAny<Ticket>()), Times.Never);
            
        }
        [Fact]
        public void ShouldNotPurchaseTicketEventSoldOut()
        {
            // Arrange
            PurchaseTicketViewModel viewModel = new PurchaseTicketViewModel()
            {
                EventID = 1,
                EventName = "Concert",
                EventDateTime = DateTime.Now.AddDays(1),
                Price = 50.00m
            };
            Ticket newTicket = null;
            this.mockTicketRepo.Setup(x => x.AddTicket(It.IsAny<Ticket>())).Callback<Ticket>(t => newTicket = t);


            Event @event = new Event()
            {
                EventID = viewModel.EventID,
                EventName = viewModel.EventName,
                DateTime = viewModel.EventDateTime,
                GeneralAdmissionPrice = 100.00m,
                OpenSeatingAvailable = true,
                Venue = new Venue()
                {
                    Capacity = 1
                }
            };
            @event.Tickets.Add(new Ticket());

            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(@event);

            //Act
            this.controller.PurchaseTicket(viewModel);
            //Assert
            this.mockTicketRepo.Verify(x => x.AddTicket(It.IsAny<Ticket>()), Times.Never);

        }
    }

}
