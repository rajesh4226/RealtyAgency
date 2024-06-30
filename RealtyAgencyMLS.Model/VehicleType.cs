using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblVehicleType")]
    public class VehicleType : BaseModel
    {
        public string VehicleTypeName { get; set; }
    }
}
