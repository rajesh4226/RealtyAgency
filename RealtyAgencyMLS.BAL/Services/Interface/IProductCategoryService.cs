using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IProductCategoryService
    {
            Task<IEnumerable<ProductCategory>> GetAll();

            Task<ProductCategory> GetById(int id);

            Task<int> Add(ProductCategory productCategory);

            Task<bool> Update(ProductCategory productCategory);

            Task<bool> Delete(ProductCategory productCategory);

            Task<IEnumerable<ProductCategory>> GetAllByQuery(string procedureName, DynamicParameters param);

            Task<IEnumerable<ProductCategoryDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
