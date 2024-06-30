using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IMvcControllerDiscovery
    {
        IEnumerable<MvcControllerInfo> GetControllers();
    }
}
