using Microsoft.AspNetCore.Mvc;
using Moq;
using SP26ConcertClassLibraryMartino;
using SP26ConcertMVCMartino.Controllers;
using SP26ConcertMVCMartino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertTestMartino
{
    public class VenueTest
    {
        //TDD - Test Driven Development

        //Red
        //Green
        //Refactor
        [Fact]
        public void ShouldListAllVenues()
        {
            //AAA approach

            //Arrange
            List<Venue> expectedVenues = new List<Venue>();
            expectedVenues.Add(new Venue("Test1", "TestAddress1", null, "City", "State", "Zip", "USA", VenueType.Indoor, 100));
            expectedVenues.Add(new Venue("Test2", "TestAddress2", null, "City", "State", "Zip", "USA", VenueType.Outdoor, 200));

            //Prepare an iVenueRepo
            //Mock
            Mock<IVenueRepo> mockVenueRepo = new Mock<IVenueRepo>();
            //Prepare/setup GetAllVenues
            mockVenueRepo.Setup(m => m.GetAllVenues()).Returns(expectedVenues);

            //Act
            VenueController controller = new VenueController(mockVenueRepo.Object);
            ViewResult result = controller.ListAllVenues() as ViewResult;
            List<Venue> actualVenues = result.Model as List<Venue>;

            //Assert
            Assert.Equal(expectedVenues.Count, actualVenues.Count);
        }
    }
}
