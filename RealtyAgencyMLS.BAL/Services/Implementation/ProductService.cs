using Dapper;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.DAL.Repository;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<ProductDTO> _viewrepository;

        public ProductService(IRepository<Product> repository, IRepository<ProductDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(Product product)
        {
            return await _repository.AddAsync(product);
        }

        public async Task<bool> Delete(Product product)
        {
            return await _repository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<Product> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Product product)
        {
            return await _repository.UpdateAsync(product);
        }
    }
}
