using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealtyAgencyMLS.Model
{
    [Table("tblRealtyAgents")]
    public class RealtyAgents : BaseModel
    {
        public int AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Status { get; set; }
        public string LicenseID { get; set; }
        public string OrganizationName { get; set; }
        public string OrgAddressStreet { get; set; }
        public string OrgCity { get; set; }
        public string OrgState { get; set; }
        public string OrgZip { get; set; }
        public string OrgPhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string HiddenUsCID { get; set; }
        public string AboutUs { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIN { get; set; }
        public int AppUserID { get; set; }
        public bool IsMLSAgent { get; set; }
        public string FullName { get; set; }
        public string ApplicationUrl { get; set; }

        //public bool IsMLS { get; set; }

    }
}
