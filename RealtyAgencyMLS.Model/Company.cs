using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblCompany")]
    public class Company : BaseModel
    {
        public string CompanyName { get; set; }
    }
}
