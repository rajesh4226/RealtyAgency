using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class VehicleCreateModel
    {
        public int PID { get; set; }

        [Required(ErrorMessage = "Enter VehicleName")]
        public string VehicleName { get; set; }
        public double Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int CompanyID { get; set; }
        public int VehicleTypeID { get; set; }
    }
}
