using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IUserManageServices
    {
        Task<int> Add(RealtyAgents  realtyAgents);

        Task<RealtyAgents> GetById(int appUserID);

        Task<bool> Update(RealtyAgents realtyAgents);

        Task<bool> Delete(RealtyAgents realtyAgents);

        Task<IEnumerable<RealtyAgentsDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
        Task <RealtyAgents> GetSingleAgent(string procedureName, DynamicParameters param);
    }
}
