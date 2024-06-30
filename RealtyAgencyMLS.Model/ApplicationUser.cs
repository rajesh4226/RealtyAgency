using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppUserID { get; set; }
        public bool IsFirstLogin { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string AddressStreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string ImagePath { get; set; }
        public string Discriminator { get; set; }
    }
}
