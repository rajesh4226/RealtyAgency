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
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IRepository<BlogCategory> _repository;
        private readonly IRepository<BlogCategoryDTO> _viewrepository;

        public BlogCategoryService(IRepository<BlogCategory> repository, IRepository<BlogCategoryDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(BlogCategory blogCategory)
        {
            return await _repository.AddAsync(blogCategory);
        }

        public async Task<bool> Delete(BlogCategory blogCategory)
        {
            return await _repository.DeleteAsync(blogCategory);
        }

        public async Task<IEnumerable<BlogCategory>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<BlogCategory>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<BlogCategoryDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<BlogCategory> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(BlogCategory blogCategory)
        {
            return await _repository.UpdateAsync(blogCategory);
        }
    }
}
