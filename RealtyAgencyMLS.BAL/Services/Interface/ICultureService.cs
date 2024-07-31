using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface ICultureService
    {
        Task<IEnumerable<Culture>> GetAll();

        Task<Culture> GetById(int id);

        Task<int> Add(Culture culture);

        Task<bool> Update(Culture culture);

        Task<bool> Delete(Culture culture);

        Task<IEnumerable<Culture>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<CultureDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
        Task<SingleCultureDTO> GetSingleCultureDetailsList(string procedureName, DynamicParameters param);
    }
}
