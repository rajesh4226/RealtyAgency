using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAll();

        Task<Company> GetById(int id);

        Task<int> Add(Company company);

        Task<bool> Update(Company company);

        Task<bool> Delete(Company company);

        Task<IEnumerable<Company>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<CompanyDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
