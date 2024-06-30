using Dapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.Services.Interface
{
    public interface IVehicleTypeService
    {

        Task<IEnumerable<VehicleType>> GetAll();

        Task<VehicleType> GetById(int id);

        Task<int> Add(VehicleType vehicletype);

        Task<bool> Update(VehicleType vehicletype);

        Task<bool> Delete(VehicleType vehicletype);

        Task<IEnumerable<VehicleType>> GetAllByQuery(string procedureName, DynamicParameters param);

        Task<IEnumerable<VehicleTypeDTO>> GetAllForDisplayByQuery(string procedureName, DynamicParameters param);
    }
}
