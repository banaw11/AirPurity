using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public  interface IStationRepository
    {
        Task<StationClientDTO> GetStationsByIdAsync(int stationId);
        Task<StationStateDTO> GetStationState(int stationId);
    }
}
