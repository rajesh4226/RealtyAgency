using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class BlogCategoryDTO : BaseModel
    {
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public int TotalRecords { get; set; }
    }
}
