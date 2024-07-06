using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Web.Models
{
    public class BlogViewModel
    {
        public IEnumerable<BlogDTO> Blogs { get; set; }
        public int TotalPages { get; set; }
        public int Totalrecords { get; set; }
        public int CurrentPage { get; set; }
    }
}
