using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblVehicle")]
    public class Vehicle : BaseModel
    {
        public string VehicleName { get; set; }
        public int CompanyID { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int VehicleTypeID { get; set; }
    }
}
