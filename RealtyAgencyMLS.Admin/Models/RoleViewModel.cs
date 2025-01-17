﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
