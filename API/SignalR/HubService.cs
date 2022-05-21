using AirPurity.API.Interfaces;
using API.DTOs.Pagination;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class HubService : IHubService
    {
        private readonly IHubContext<OnlineClientHub> _hubContext;
        private readonly IStationService _stationService;
        private readonly ISensorService _sensorService;
        private readonly OnlineTracker _onlineTracker;

        public HubService(IHubContext<OnlineClientHub> hubContext,
            IStationService stationService, 
            ISensorService sensorService,
            OnlineTracker onlineTracker)
        {
            _hubContext = hubContext;
            _stationService = stationService;
            _sensorService = sensorService;
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

            var sensorsDailyData = await _sensorService.GetSensorsDataAsync(sensorsDataQuery);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirData", sensorsDailyData);


            var stationState = await _stationService.GetStationStateAsync(clientDto.StationId);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirQuality", stationState);
        }


    }
}
