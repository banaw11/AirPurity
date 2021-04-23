using API.DTOs;
using API.DTOs.Pagination;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public  interface ISensorRepository
    {
        Task<ICollection<SensorDataDTO>> GetSensorsData(SensorsDataQuery query);
        Task<ICollection<SensorDTO>> GetSensors(int stationId);
        Task<ICollection<Norm>> GetNormsAsync();
    }
}
