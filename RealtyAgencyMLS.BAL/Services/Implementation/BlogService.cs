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
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<BlogDTO> _viewrepository;

        public BlogService(IRepository<Blog> repository, IRepository<BlogDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(Blog blog)
        {
            return await _repository.AddAsync(blog);
        }

        public async Task<bool> Delete(Blog blog)
        {
            return await _repository.DeleteAsync(blog);
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<BlogDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<Blog> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Blog blog)
        {
            return await _repository.UpdateAsync(blog);
        }
    }
}
