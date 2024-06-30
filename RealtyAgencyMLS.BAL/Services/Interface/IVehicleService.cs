using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAll();

        Task<Vehicle> GetById(int id);

        Task<int> Add(Vehicle vehicle);

        Task<bool> Update(Vehicle vehicle);

        Task<bool> Delete(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<VehicleDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
