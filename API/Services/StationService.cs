using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.BusinessLogic.External.Services;
using AirPurity.API.BusinessLogic.Repositories.Repositories;
using AirPurity.API.Common.Exceptions;
using AirPurity.API.Interfaces;
using API.DTOs.ClientDTOs;
using AutoMapper;
using System.Threading.Tasks;

namespace AirPurity.API.Services
{
    public class StationService : IStationService
    {
        private readonly StationRepository _stationRepository;
        private readonly GiosHttpClientService _giosContext;
        private readonly IMapper _mapper;

        public StationService(StationRepository stationRepository, GiosHttpClientService giosContext, IMapper mapper)
        {
            _stationRepository = stationRepository;
            _giosContext = giosContext;
            _mapper = mapper;
        }

        public StationClientDTO GetStationsById(int stationId)
        {
            var station = _stationRepository.GetById(stationId);
            if (station is null) throw new NotFoundException($"Station with ID [{stationId}] not found");

            var stationDTO = _mapper.Map<StationClientDTO>(station);
            return stationDTO;
        }

        public async Task<StationState> GetStationStateAsync(int stationId)
        {
            var station = GetStationsById(stationId);
            if (station is null) throw new NotFoundException($"Station with ID [{stationId}] not found");

            var stationState = await _giosContext.GetStationState(stationId);

            return stationState;
        }
    }
}
