using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblCulture")]
    public class Culture : BaseModel
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string YoutubeVideoLink { get; set; }
    }
}
