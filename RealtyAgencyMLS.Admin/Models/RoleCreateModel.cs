﻿using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class RoleCreateModel
    {

        public string RoleId { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Name { get; set; }

        public List<MvcControllerInfo> SelectedControllers { get; set; } = new List<MvcControllerInfo>();
    }
}
