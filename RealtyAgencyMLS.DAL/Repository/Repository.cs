using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealtyAgencyMLS.DAL.Connection;
using RealtyAgencyMLS.DAL.Data;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.Common;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.DAL.Repository
{
    public class Repository<T> : IDisposable, IRepository<T> where T : BaseModel
    {
        private readonly IConnectionFactory _connectionFactory;
        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.InsertAsync(entity);
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.DeleteAsync(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.GetAllAsync<T>();
            }
        }

        public async Task<T> GetByIdAsync(int pid)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.GetAsync<T>(pid);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.UpdateAsync(entity);
            }
        }
        public async Task<int> DeleteAll(string tableName)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.ExecuteAsync("TRUNCATE TABLE " + tableName);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<bool> IsExists(string procedureName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.ExecuteScalarAsync<bool>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<T> GetByQuery(string procedureName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(_connectionFactory.GetConnectionString()))
            {
                return await conn.QueryFirstOrDefaultAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        

        public async Task<bool> AddAsync(List<T> entity, string tableName)
        {
            return await Task.FromResult(true);
        }
    }
}
