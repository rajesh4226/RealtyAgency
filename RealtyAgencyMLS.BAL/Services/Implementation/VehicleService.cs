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
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IRepository<VehicleDTO> _viewrepository;

        public VehicleService(IRepository<Vehicle> repository, IRepository<VehicleDTO> viewrepository)
        {
            _repository = repository;
            _viewrepository = viewrepository;
        }
        public async Task<int> Add(Vehicle vehicle)
        {
            try
            {
                return await _repository.AddAsync(vehicle);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> Delete(Vehicle vehicle)
        {
            return await _repository.DeleteAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllByQuery(string procedureName, DynamicParameters param)
        {
            return await _repository.GetAllByQuery(procedureName, param);
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param)
        {
            try
            {
                return await _viewrepository.GetAllByQuery(procedureName, param);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<Vehicle> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Vehicle vehicle)
        {
            return await _repository.UpdateAsync(vehicle);
        }
    }
}