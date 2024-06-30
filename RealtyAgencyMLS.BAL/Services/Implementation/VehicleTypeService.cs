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
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IRepository<VehicleType> _repository;
        private readonly IRepository<VehicleTypeDTO> _viewrepository;

        public VehicleTypeService(IRepository<VehicleType> repository, IRepository<VehicleTypeDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(VehicleType vehicletype)
        {
            return await _repository.AddAsync(vehicletype);
        }

        public async Task<bool> Delete(VehicleType vehicletype)
        {
            return await _repository.DeleteAsync(vehicletype);
        }

        public async Task<IEnumerable<VehicleType>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleType>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            return await _viewrepository.GetAllByQuery(procedureName, param);
        }

        public async Task<VehicleType> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(VehicleType vehicletype)
        {
            return await _repository.UpdateAsync(vehicletype);
        }
    }
}