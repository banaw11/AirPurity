using AirPurity.API.BusinessLogic.External.Models;
using API.DTOs.ClientDTOs;
using System.Threading.Tasks;

namespace AirPurity.API.Interfaces
{
    public interface IStationService 
    {
        StationClientDTO GetStationsById(int stationId);
        Task<StationState> GetStationStateAsync(int stationId);
    }
}
