using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class ProductDTO : BaseModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public DateTime MFG { get; set; }
        public DateTime EXP { get; set; }
        public string Heading { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public string ProductUrl { get; set; }
        public string ShortDescription { get; set; }
        public int TotalRecords { get; set; }
    }
}
