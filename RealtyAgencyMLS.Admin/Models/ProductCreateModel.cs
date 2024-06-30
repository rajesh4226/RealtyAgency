using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class ProductCreateModel
    {
        public int PID { get; set; }
        public string ProductName { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Enter Product title!")]
        public string Heading { get; set; }
        public double Price { get; set; }
        public DateTime MFG { get; set; }
        public DateTime EXP { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Tags (Separeted with commas(,))")]
        public string Tags { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
    }
}