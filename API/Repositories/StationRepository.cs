using API.Data;
using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Entities;
using API.Interfaces;
using API.Middleware.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly DataContext _context;
        private readonly IExternalClientContext _clientContext;
        private readonly IMapper _mapper;

        public StationRepository(DataContext context, IExternalClientContext clientContext, IMapper mapper)
        {
            _context = context;
            _clientContext = clientContext;
            _mapper = mapper;
        }

        public async Task<City> GetCityByNameAsync(string cityName)
        {
            var city = await _context.Cities.Where(x => x.Name.ToLower() == cityName.ToLower())
                .Include(x => x.Stations)
                .Include(x => x.Commune)
                .FirstOrDefaultAsync();

           if(city is null) throw new NotFoundException($"City [{cityName}] not found");

            return city;
        }

        public async Task<int> GetCityIdByNameAsync(string cityName)
        {
            var city = await _context.Cities
                .Where(x => x.Name.ToLower() == cityName.ToLower())
                .FirstOrDefaultAsync();

            if(city is null) throw new NotFoundException($"City [{cityName}] not found");

            return city.Id;
        }

        public async Task<ICollection<StationClientDTO>> GetStationsByCityAsync(string cityName)
        {
            var city = await GetCityByNameAsync(cityName);
            var stations = _mapper.Map<ICollection<StationClientDTO>>(city.Stations);

            return stations;
        }

        public async Task<StationClientDTO> GetStationsByIdAsync(int stationId)
        {
            var station = await _context.Stations.Where(x => x.Id == stationId).FirstOrDefaultAsync();
            if(station is null) throw new NotFoundException($"Station with ID [{stationId}] not found");

            var stationDTO = _mapper.Map<StationClientDTO>(station);
            return  stationDTO;
        }

        public async Task<StationStateDTO> GetStationState(int stationId)
        {
            var station = await GetStationsByIdAsync(stationId);
            if(station is null) throw new NotFoundException($"Station with ID [{stationId}] not found");

            var stationState = await _clientContext.GetStationState(stationId);

            return stationState;
        }
    }
}
