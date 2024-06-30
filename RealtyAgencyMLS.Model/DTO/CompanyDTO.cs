using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class CompanyDTO : BaseModel
    {
        public string CompanyName { get; set; }
        public int TotalRecords { get; set; }

    }
}
