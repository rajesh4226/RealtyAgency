using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblProducts")]
    public class Product : BaseModel
    {
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime MFG { get; set; }
        public DateTime EXP { get; set; }
        public string Heading { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public string ProductUrl { get; set; }
        public string ShortDescription { get; set; }
    }
}
