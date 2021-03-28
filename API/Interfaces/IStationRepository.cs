using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
     public  interface IStationRepository
    {
        Task<City> GetCityByNameAsync(string cityName);
        Task<int> GetCityIdByNameAsync(string cityName);
        Task<ICollection<StationClientDTO>> GetStationsByCityAsync(string cityName);
        Task<StationStateDTO> GetStationState(int stationId);
    }
}
