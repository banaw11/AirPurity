using API.DTOs.Pagination;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class HubRepository : IHubRepository
    {
        private readonly IHubContext<OnlineClientHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly OnlineTracker _onlineTracker;

        public HubRepository(IHubContext<OnlineClientHub> hubContext, IUnitOfWork unitOfWork, OnlineTracker onlineTracker)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
            _onlineTracker = onlineTracker;
        }

        public ICollection<ClientDto> GetOnlineClients()
        {
            return _onlineTracker.GetOnlineClients();
        }

        public async Task RefreshClientsData()
        {
            var clientsDto = GetOnlineClients();

            if (clientsDto.Any())
                foreach (var clientDto in clientsDto)
                {
                    await RefreshStationData(clientDto);
                }
        }

        public async Task RefreshStationData(ClientDto clientDto)
        {
            var sensorsDataQuery = new SensorsDataQuery()
            {
                StationId = clientDto.StationId,
                Range = RangeOfData.DAY
            };

            var sensorsDailyData = await _unitOfWork.SensorRepository.GetSensorsData(sensorsDataQuery);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirData", sensorsDailyData);


            var stationState = await _unitOfWork.StationRepository.GetStationState(clientDto.StationId);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirQuality", stationState);
        }


    }
}
