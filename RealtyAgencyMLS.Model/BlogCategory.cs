using Dapper.Contrib.Extensions;
using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblBlogCategory")]
    public class BlogCategory : BaseModel
    {
        public string CategoryName { get; set; }
    }
}
