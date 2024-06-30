﻿using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class VehicleDTO : BaseModel
    {
        public string VehicleName { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int CompanyID { get; set; }
        public int VehicleTypeID { get; set; }
        public int TotalRecords { get; set; }
    }
}
