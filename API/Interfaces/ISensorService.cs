using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.Data.Entities;
using API.DTOs;
using API.DTOs.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPurity.API.Interfaces
{
    public interface ISensorService
    {
        Task<ICollection<SensorData>> GetSensorsDataAsync(SensorsDataQuery query);
        Task<ICollection<SensorExternal>> GetSensors(int stationId);
        ICollection<Norm> GetNorms();
    }
}
