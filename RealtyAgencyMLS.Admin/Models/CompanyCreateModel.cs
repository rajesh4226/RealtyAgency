using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class CompanyCreateModel
    {
        public int PID { get; set; }

        [Required(ErrorMessage = "Enter Company name!")]
        public string CompanyName { get; set; }

    }
}
