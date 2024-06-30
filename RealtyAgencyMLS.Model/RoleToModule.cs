using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    [Table("tblRoleToModule")] 
    public class RoleToModule : BaseModel
    {
        public string RoleId { get; set; }
        public int ModuleId { get; set; }
    }
}
