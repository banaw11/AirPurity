using System;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class OnlineClientHub : Hub
    {
        private readonly OnlineTracker _onlineTracker;
        private readonly IUnitOfWork _unitOfWork;

        public OnlineClientHub(OnlineTracker onlineTracker, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _onlineTracker = onlineTracker;

        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var query = httpContext.Request.Query.TryGetValue("stationId", out var stationId);
            if(query)
                await _onlineTracker.ClientConnected(Context.ConnectionId, int.Parse(stationId));

            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _onlineTracker.ClientDisconnected(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ResfreshStationData(string connectionId, int stationId)
        {
            var sensorsData = await _unitOfWork.SensorRepository.GetSensorsData(stationId);
            await Clients.Client(connectionId).SendAsync("RefreshedAirData", sensorsData);

            var stationState = await _unitOfWork.StationRepository.GetStationState(stationId);
            await Clients.Client(connectionId).SendAsync("RefreshedAirQuality", stationState);
        }
    }
}