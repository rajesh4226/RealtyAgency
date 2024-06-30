using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class BlogCreateModel
    {
        public int PID { get; set; }

        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Enter blog title!")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "Enter blog description!")]
        public string Description { get; set; }


        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "Enter short description!")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Enter blog tags!")]
        [Display(Name = "Tags (Separeted with commas(,))")]
        public string Tags { get; set; }

        [Display(Name = "Blog Image")]
        [Required(ErrorMessage = "Choose blog Image!")]
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
    }
}
