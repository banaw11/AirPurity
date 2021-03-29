using API.DTOs;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public  interface ISensorRepository
    {
        Task<ICollection<SensorDataDTO>> GetSensorsData(int stationId);
        Task<ICollection<SensorDTO>> GetSensors(int stationId);
        Task<ICollection<Norm>> GetNormsAsync();
    }
}
