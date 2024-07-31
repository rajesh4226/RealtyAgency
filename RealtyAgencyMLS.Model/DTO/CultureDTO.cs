using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.DTO
{
    public class CultureDTO : BaseModel
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string YoutubeVideoLink { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
