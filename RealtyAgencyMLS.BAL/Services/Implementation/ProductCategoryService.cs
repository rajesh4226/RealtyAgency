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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<ProductCategory> _repository;
        private readonly IRepository<ProductCategoryDTO> _viewrepository;

        public ProductCategoryService(IRepository<ProductCategory> repository, IRepository<ProductCategoryDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(ProductCategory productCategory)
        {
            return await _repository.AddAsync(productCategory);
        }

        public async Task<bool> Delete(ProductCategory productCategory)
        {
            return await _repository.DeleteAsync(productCategory);
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<ProductCategory> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(ProductCategory productCategory)
        {
            return await _repository.UpdateAsync(productCategory);
        }
    }
}
