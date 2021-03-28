using API.Data;
using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
            return await _context.Cities.Where(x => x.Name.ToLower() == cityName.ToLower())
                .Include(x => x.Stations)
                .Include(x => x.Commune)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCityIdByNameAsync(string cityName)
        {
            return await _context.Cities
                .Where(x => x.Name.ToLower() == cityName.ToLower())
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<StationClientDTO>> GetStationsByCityAsync(string cityName)
        {
            var city = await GetCityByNameAsync(cityName);
            return _mapper.Map<ICollection<StationClientDTO>>(city.Stations);
        }

        public async Task<StationStateDTO> GetStationState(int stationId)
        {
            return await _clientContext.GetStationState(stationId);
        }
    }
}
