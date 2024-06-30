using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model
{
    public class ApplicationRole : IdentityRole
    {
        public string Access { get; set; }
    }
}
