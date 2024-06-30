using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAll();

        Task<Blog> GetById(int id);

        Task<int> Add(Blog blog);

        Task<bool> Update(Blog blog);

        Task<bool> Delete(Blog blog);

        Task<IEnumerable<Blog>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<BlogDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}

