using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealtyAgencyMLS.BAL.EmailServices;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.DAL.Data;
using RealtyAgencyMLS.DAL.Repository;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.Common;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Implementation
{
    public class UserManageServices : IUserManageServices
    {
        private readonly IRepository<RealtyAgents> _repository;
        private readonly IRepository<RealtyAgentsDTO> _viewrepository;

        public UserManageServices(IRepository<RealtyAgents> repository, IRepository<RealtyAgentsDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(RealtyAgents realtyAgents)
        {
            return await _repository.AddAsync(realtyAgents);
        }

        public async Task<RealtyAgents> GetById(int appUserID)
        {
            return await _repository.GetByIdAsync(appUserID);
        }
        public async Task<bool> Update(RealtyAgents realtyAgents)
        {
            return await _repository.UpdateAsync(realtyAgents);
        }

        public async Task<bool> Delete(RealtyAgents realtyAgents)
        {
            return await _repository.DeleteAsync(realtyAgents);
        }

        public async Task<IEnumerable<RealtyAgentsDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<RealtyAgents> GetSingleAgent(string procedureName, DynamicParameters param)
        {
            return await _repository.GetByQuery(procedureName, param);
        }
    }
}
