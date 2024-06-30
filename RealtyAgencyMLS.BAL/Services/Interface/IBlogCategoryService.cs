using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategory>> GetAll();

        Task<BlogCategory> GetById(int id);

        Task<int> Add(BlogCategory blogCategory);

        Task<bool> Update(BlogCategory blogCategory);

        Task<bool> Delete(BlogCategory blogCategory);

        Task<IEnumerable<BlogCategory>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<BlogCategoryDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
