using System;
using System.Collections.Generic;
using System.Text;
using RealtyAgencyMLS.Model.Common;

namespace RealtyAgencyMLS.Model.DTO
{
    public class ProductCategoryDTO :BaseModel
    {
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public int TotalRecords { get; set; }
    }
}
