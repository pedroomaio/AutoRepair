using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoRepair.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string LastName { get; set; }


        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public bool IdAmin { get; set; }
        public bool IdClient { get; set; }
        public bool IdEmployee { get; set; }

        //public int PostalCodeId { get; set; }
        //public PostalCode PostalCode { get; set; }
    }
}
