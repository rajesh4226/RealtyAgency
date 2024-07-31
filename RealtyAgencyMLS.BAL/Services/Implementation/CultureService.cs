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
    public class CultureService : ICultureService
    {
        private readonly IRepository<Culture> _repository;
        private readonly IRepository<CultureDTO> _viewrepository;
        public CultureService(IRepository<Culture> repository, IRepository<CultureDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(Culture culture)
        {
            return await _repository.AddAsync(culture);
        }

        public async Task<bool> Delete(Culture culture)
        {
            return await _repository.DeleteAsync(culture);
        }

        public async Task<IEnumerable<Culture>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Culture>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<CultureDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<Culture> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<SingleCultureDTO> GetSingleCultureDetailsList(string procedureName, DynamicParameters param)
        {
            return _repository.GetSingleCultureDetails(procedureName, param);
        }

        public async Task<bool> Update(Culture culture)
        {
            return await _repository.UpdateAsync(culture);
        }
    }
}
