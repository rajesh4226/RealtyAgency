using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
        public class ProductCategoryCreateModel
        {
            public int PID { get; set; }

            [Required(ErrorMessage = "Enter category name!")]
            public string CategoryName { get; set; }

            [ViewData]
            public string StatusMessage { get; set; }
        }
}
