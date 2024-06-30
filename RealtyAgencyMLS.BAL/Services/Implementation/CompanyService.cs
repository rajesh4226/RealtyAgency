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
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _repository;
        private readonly IRepository<CompanyDTO> _viewrepository;

        public CompanyService(IRepository<Company> repository, IRepository<CompanyDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(Company company)
        {
            return await _repository.AddAsync(company);
        }

        public async Task<bool> Delete(Company company)
        {
            return await _repository.DeleteAsync(company);
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Company>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<Company> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Company company)
        {
            return await _repository.UpdateAsync(company);
        }
    }
}
