using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Web.Models
{
    public class SingleBlogDisplayModel
    {
        public BlogDTO Blog { get; set; }
        public IEnumerable<BlogCategoryDTO> BlogCategory { get; set; }
        public IEnumerable<BlogDTO> RecentBlogs { get; set; }
        public IEnumerable<BlogDTO> RelatedBlogs { get; set; }
    }
}
