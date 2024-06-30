using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.BAL.Common
{
    public interface IIdentifierProvider
    {
        int DecodeId(string urlId);
        string EncodeId(int id);
    }
}
