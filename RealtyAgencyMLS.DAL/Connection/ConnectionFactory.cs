using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RealtyAgencyMLS.DAL.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection _connection;
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }
        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
