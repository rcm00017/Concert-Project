using Moq;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Controllers;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertTestMartino
{
    public class EventTest
    {
        private Mock<IEventRepo> mockEventRepo;
        private EventController controller;

        public EventTest()
        {
            this.mockEventRepo = new Mock<IEventRepo>();
            this.controller = new EventController(mockEventRepo.Object);
        }
        [Fact]
        public void ShouldSearchEvents()
        {
            //AAA
            //Arrange
            int expected = 1;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.DateTime = new DateTime(2026, 08, 22);

            List<Event> events = new List<Event>();
            Venue venue = new Venue("Venue1", "add1", null, "City", "State", "Zip", "USA", VenueType.Indoor, 100);
            events.Add(new Event(venue, "Event1", new DateTime(2026, 08, 22), false, 100));
            events.Add(new Event(venue, "Event2", new DateTime(2026, 06, 21), false, 100));
            events.Add(new Event(venue, "Event2", null, false, 100));


            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(events);

            //Act
            this.controller.SearchEvents(viewModel);
            

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }
    }
}
