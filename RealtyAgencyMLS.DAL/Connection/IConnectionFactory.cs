﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RealtyAgencyMLS.DAL.Connection
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
        void CloseConnection();
        string GetConnectionString();
    }
}
