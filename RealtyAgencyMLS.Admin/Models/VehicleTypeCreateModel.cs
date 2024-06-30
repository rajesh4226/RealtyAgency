using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class VehicleTypeCreateModel
    {
        public int PID { get; set; }
        [Required(ErrorMessage = "Enter VehicleTypeName")]
        public string  VehicleTypeName { get; set; }

    }
}
