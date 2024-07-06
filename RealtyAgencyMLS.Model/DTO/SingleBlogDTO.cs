using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
   public class SingleBlogDTO
    {
        public BlogDTO Blog { get; set; }
        public IEnumerable<BlogCategoryDTO> BlogCategory { get; set; }
        public IEnumerable<BlogDTO> RecentBlogs { get; set; }
        public IEnumerable<BlogDTO> RelatedBlogs { get; set; }
    }
}
