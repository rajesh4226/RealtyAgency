using Dapper.Contrib.Extensions;
using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace RealtyAgencyMLS.Model
{
    [Table("tblBlogs")]
    public class Blog : BaseModel
    {
        public int CategoryID { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public string BlogUrl { get; set; }
        public string ShortDescription { get; set; }
    }
}
