using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.Common
{
    public class BaseModel
    {
        [Key]
        public int PID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string ModifiedBy { get; set; }

    }
}
