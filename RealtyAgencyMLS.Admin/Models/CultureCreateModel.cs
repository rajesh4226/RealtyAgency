using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RealtyAgencyMLS.Admin.Models
{
    public class CultureCreateModel
    {
        public int PID { get; set; }

        [Required(ErrorMessage = "Enter Culture title!")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "Enter Culture description!")]
        public string Description { get; set; }
        [Display(Name = "Culture Image")]
        [Required(ErrorMessage = "Choose Culture Image!")]
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
        public string YoutubeVideoLink { get; set; }

    }
}
