using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class UserViewModel
    {
        //User
        public string UserId { get; set; }
        public int AppUserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        //role
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rolename { get; set; }
        public string RoleId { get; set; }
        public string FullName { get; set; }

        public string LicenseID { get; set; }

        [Required]
        public string AddressStreet { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string CCountry { get; set; }

        [RegularExpression(@"^\d{3}\s?\d{3}$", ErrorMessage = "Not a valid zip code")]
        public string Zip { get; set; }
        public string OrganizationName { get; set; }
        public string OrgAddressStreet { get; set; }
        public string OrgCity { get; set; }
        public string OrgState { get; set; }
        public string OrgZip { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Organization Phone")]
        public string OrgPhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string AboutUs { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIN { get; set; }
        public int AllAgentID { get; set; }
        public bool IsMLSAgent { get; set; }

        [Display(Name = "User Image")]
        public IFormFile ImageFile { get; set; }

    }
}
