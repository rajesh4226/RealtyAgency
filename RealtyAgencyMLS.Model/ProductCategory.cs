using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    
        [Table("tblProductCategory")]
        public class ProductCategory : BaseModel
        {
            public string CategoryName { get; set; }
        }
}
