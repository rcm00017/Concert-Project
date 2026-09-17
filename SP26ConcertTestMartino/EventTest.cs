using Humanizer.Localisation;
using Moq;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Controllers;
using SP26ConcertMVCMartino.Models;
using SP26ConcertMVCMartino.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertTestMartino
{
    public class EventTest
    {
        private Mock<IEventRepo> mockEventRepo;
        private Mock<IArtistRepo> mockArtistRepo;
        private Mock<IVenueRepo> mockVenueRepo;
        private Mock<IGenreRepo> mockGenreRepo;
        private EventController controller;


        public EventTest()
        {
            this.mockEventRepo = new Mock<IEventRepo>();
            this.mockArtistRepo = new Mock<IArtistRepo>();
            this.mockVenueRepo = new Mock<IVenueRepo>();
            this.mockGenreRepo = new Mock<IGenreRepo>();
            this.controller = new EventController(mockEventRepo.Object, mockArtistRepo.Object, mockVenueRepo.Object, mockGenreRepo.Object);
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
            this.mockArtistRepo.Setup(m => m.GetAllArtists()).Returns(new List<Artist>());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(new List<Venue>());

            //Act
            this.controller.SearchEventsResult(viewModel);


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
            this.mockArtistRepo.Setup(m => m.GetAllArtists()).Returns(new List<Artist>());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(new List<Venue>());
            this.mockGenreRepo.Setup(m => m.GetAllGenres()).Returns(new List<Genre>());



            //Act
            this.controller.SearchEventsResult(viewModel);


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
            this.controller.SearchEventsResult(viewModel);


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
            this.controller.SearchEventsResult(viewModel);


            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);

        }
        [Fact]
        public void ShouldSearchEventsByVenue()
        {
            //AAA
            //Arrange
            int expected = 4;
            SearchEventsViewModel viewModel = new SearchEventsViewModel();
            viewModel.VenueID = 1;




            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());

            //Act
            this.controller.SearchEventsResult(viewModel);


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
            events.Add(new Event(venue, "AddTestDuplicate", DateTime.Today.AddDays(1), false, 100m));


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

        public List<Venue> CreateMockVenues()
        {
            List<Venue> venues = new List<Venue>();
            Venue venue = new Venue();
            venue.VenueID = 1;
            venues.Add(venue);


            venue = new Venue();
            venue.VenueID = 2;
            venues.Add(venue);

            return venues;
        }

        [Fact] // Happy Path
        public void ShouldAddEvent()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = DateTime.Now.AddDays(1);
            viewModel.OpenSeatingAvailable = true;

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Event @event = null;
            this.mockEventRepo.Setup(m => m.AddEvent(It.IsAny<Event>())).Callback<Event>(e => @event = e);
            //Act
            this.controller.AddEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.AddEvent(It.IsAny<Event>()), Times.Once);
            Assert.Equal(viewModel.EventName, @event.EventName);
            Assert.Equal(viewModel.DateTime, @event.DateTime);
            Assert.Equal(viewModel.VenueID, @event.Venue.VenueID);
            Assert.Equal(viewModel.OpenSeatingAvailable, @event.OpenSeatingAvailable);
        }
        [Fact]
        public void ShouldNotAddEventDuplicate()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = DateTime.Today.AddDays(1);
            viewModel.OpenSeatingAvailable = true;

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Event @event = null;
            this.mockEventRepo.Setup(m => m.AddEvent(It.IsAny<Event>())).Callback<Event>(e => @event = e);
            //Act
            this.controller.AddEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.AddEvent(It.IsAny<Event>()), Times.Never);
            //Assert.Null(@event);
            Assert.Contains("An event at this location and date/time already exists!", controller.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage));

        }
        [Fact]
        public void ShouldNotAddEventPast()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = new DateTime(2025, 01, 01);
            viewModel.OpenSeatingAvailable = true;

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Event @event = null;
            this.mockEventRepo.Setup(m => m.AddEvent(It.IsAny<Event>())).Callback<Event>(e => @event = e);
            //Act
            this.controller.AddEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.AddEvent(It.IsAny<Event>()), Times.Never);
            Assert.Null(@event);
            Assert.Contains("The date/time of this event is in the past!", controller.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage));

        }
        [Fact]
        public void ShouldNotAddEventInvalidData()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            List<ValidationResult> results = new List<ValidationResult>();
            //Act
            bool isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results);
            //Assert
            Assert.False(isValid);
            Assert.Equal("The EventName field is required.", results.First().ErrorMessage);
        }
        [Fact]
        public void ShouldEditEvent()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = DateTime.Now.AddDays(1);
            viewModel.OpenSeatingAvailable = true;
            viewModel.EventID = 1;

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Venue venue = new Venue("Test", "Line1", "Line2", "C", "S", "Z", "C", VenueType.Outdoor, 100);
            venue.VenueID = 1;
            Event existing = new Event(venue, "TestEvent", DateTime.Now.AddDays(2), true, 10m);
            existing.EventID = 1;
            
            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(existing);
            //Act
            this.controller.EditEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.EditEvent(It.IsAny<Event>()), Times.Once);
            Assert.Equal(viewModel.EventName, existing.EventName);
            Assert.Equal(viewModel.DateTime, existing.DateTime);
            Assert.Equal(viewModel.VenueID, existing.Venue.VenueID);
            Assert.Equal(viewModel.OpenSeatingAvailable, existing.OpenSeatingAvailable);
        }
        [Fact]
        public void ShouldNotEditEventDuplicate()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = DateTime.Now.AddDays(1);
            viewModel.OpenSeatingAvailable = true;
            viewModel.EventID = 1;

            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Venue venue = new Venue("Test", "Line1", "Line2", "C", "S", "Z", "C", VenueType.Outdoor, 100);
            venue.VenueID = 1;

            List<Event> existingEvents = new List<Event>();
           
            Event existing = new Event(venue, "TestEvent", DateTime.Now.AddDays(2), true, 10m);
            existing.EventID = 1;
            existingEvents.Add(existing);

            Event existing2 = new Event(venue, "TestEvent2", DateTime.Now.AddDays(1), true, 10m);
            existing2.EventID = 2;
            existingEvents.Add(existing2);

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(existingEvents);


            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(existing);
            //Act
            this.controller.EditEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.EditEvent(It.IsAny<Event>()), Times.Never);

            Assert.Contains("An event at this location and date/time already exists!", controller.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage));

        }
        [Fact]
        public void ShouldNotEditEventEventPassed()
        {
            //Arrange
            EventViewModel viewModel = new EventViewModel();
            viewModel.EventName = "Test";
            viewModel.VenueID = 1;
            viewModel.DateTime = DateTime.Now.AddDays(1);
            viewModel.OpenSeatingAvailable = true;
            viewModel.EventID = 1;

            this.mockEventRepo.Setup(m => m.GetAllEvents()).Returns(CreateMockEvents());
            this.mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(CreateMockVenues());

            Venue venue = new Venue("Test", "Line1", "Line2", "C", "S", "Z", "C", VenueType.Outdoor, 100);
            venue.VenueID = 1;
            Event existing = new Event(venue, "TestEvent", new DateTime(2026, 01, 01), true, 10m);
            existing.EventID = 1;

            this.mockEventRepo.Setup(m => m.FindEvent(viewModel.EventID)).Returns(existing);
            //Act
            this.controller.EditEvent(viewModel);

            //Assert
            this.mockEventRepo.Verify(m => m.EditEvent(It.IsAny<Event>()), Times.Never);

            Assert.Contains("This event has already taken place!", controller.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage));

        }
    }
}
