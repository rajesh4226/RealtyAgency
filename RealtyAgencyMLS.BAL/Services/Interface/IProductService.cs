using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(int id);

        Task<int> Add(Product product);

        Task<bool> Update(Product product);

        Task<bool> Delete(Product product);

        Task<IEnumerable<Product>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<ProductDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
