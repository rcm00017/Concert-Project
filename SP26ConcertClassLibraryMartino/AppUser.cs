using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP26ConcertClassLibraryMartino
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }



        
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        
        public string? City { get; set; }
        
        public string? State { get; set; }

        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string AddressFriendlyName
        {
            get
            {
                if (AddressLine1 == null && City == null)
                {
                    return "N/A";

                }
                else
                {
                    return AddressLine1 + " " + AddressLine2 + " " + City + ", " + State + " " + PostalCode + " " + Country;
                }
            }
        }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public AppUser(string email, string password, string firstName, string lastName)
        {
           this.FirstName = firstName;
            this.LastName = lastName;

            this.UserName = email;
            this.Email = email;

            //passwords are stored as hashes
            //hashes = variable length input -> fixed length output
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            this.PasswordHash = hasher.HashPassword(this, password);

            //GUID
            //Globally Unique Identifier
            //8-4-4-12
            this.SecurityStamp = new Guid().ToString();
        }
        public AppUser()
        {
            
        }
    }
}
