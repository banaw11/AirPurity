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
