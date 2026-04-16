using Humanizer.Localisation;
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

            


            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEvents(viewModel);
            

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }


        [Fact]
        public void ShouldSearchEventName()
        {
            //AAA
            //Arrange
            int expected = 1;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.EventName = "  CounTRy";
            


            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEvents(viewModel);


            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }
        [Fact]
        public void ShouldSearchEventsByArtistName()
        {
            //AAA
            //Arrange
            int expected = 1;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.ArtistID = 1;




            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEvents(viewModel);


            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }
        [Fact]
        public void ShouldSearchEventsByGenre()
        {
            //AAA
            //Arrange
            int expected = 1;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.GenreID = 1;




            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEvents(viewModel);


            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }
        [Fact]
        public void ShouldSearchEventsByVenue()
        {
            //AAA
            //Arrange
            int expected = 3;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.VenueID = 1;




            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEvents(viewModel);


            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }

        //DRY Dont Repeat yourself
        public List<Event> CreateMockEvents()
        {
            List<Event> events = new List<Event>();
            Venue venue = new Venue("Venue1", "add1", null, "City", "State", "Zip", "USA", VenueType.Indoor, 100);
            venue.VenueID = 1;

            events.Add(new Event(venue, "Event1", new DateTime(2026, 08, 22), false, 100));
            events.Add(new Event(venue, "Event2", new DateTime(2026, 06, 21), false, 100));
            events.Add(new Event(venue, "Country Music Festival", null, false, 100));


            venue = new Venue("Venue2", "add1", null, "City", "State", "Zip", "USA", VenueType.Indoor, 100);
            venue.VenueID = 2;
            Event @event = new Event(venue, "EventName", null, true, 100);
            Artist Kanye = new Artist("Kanye", "West");
            Kanye.ArtistID = 1;
            Genre genre = new Genre("Country");
            genre.GenreID = 1;
            Kanye.Genres.Add(genre);
            @event.Artists.Add(Kanye);

            events.Add(@event);

            @event = new Event(venue, "EventName", null, true, 100);
            Kanye = new Artist("Ryan", "Martino");
            Kanye.ArtistID = 2;
            @event.Artists.Add(Kanye);

            events.Add(@event);



            return events;
        }
    }
}
