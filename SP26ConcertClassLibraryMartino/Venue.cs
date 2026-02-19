using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class Venue
    {
        //Auto incremented PK
        [Key]
        public int VenueID { get; set; }


        [Required]
        public string VenueName { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State {  get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public VenueType VenueType { get; set; }
        [Required]
        public int Capacity { get; set; }

        //MANY = LIST relationship between tables
        public List<Event> Events { get; set; } = new List<Event>();

        //LIST = NULL or EMPTY


        //Helper Property
        public string AddressFriendlyName
        {
            get
            {
                return AddressLine1 + " " + AddressLine2 + " " + City + ", " + State + " " + PostalCode + " " + Country;
            }
        }

        //Constructor - initializes - Creates a NEW object
        // Parmeterized Constructor
        public Venue(string venueName, string addressline1, string? addressline2, string city, string state, string postalCode, string country, VenueType venueType, int capacity)
        {
            VenueName = venueName;
            AddressLine1 = addressline1;
            AddressLine2 = addressline2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            VenueType = venueType;
            Capacity = capacity;
            
        }

        // EF - Entity Framework
        //Classes -> Tables
        //ORM - Object Relational Mapper

        // Default Constructor
        public Venue()
        {
            
        }

    }// end class

    public enum VenueType
    {
        Indoor, Outdoor
    }
}//end namespace
