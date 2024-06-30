using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ReturnPage { get; set; }
    }
}
