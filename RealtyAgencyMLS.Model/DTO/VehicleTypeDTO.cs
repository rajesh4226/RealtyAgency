using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class VehicleTypeDTO : BaseModel
    {
        public string VehicleTypeName { get; set; }
        public int TotalRecords { get; set; }

    }
}
