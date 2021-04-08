﻿using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
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

        public async void RefreshClientsData()
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
            var sensorsData = await _unitOfWork.SensorRepository.GetSensorsData(clientDto.StationId);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirData", sensorsData);

            var stationState = await _unitOfWork.StationRepository.GetStationState(clientDto.StationId);
            await _hubContext.Clients.Client(clientDto.ConnectionId).SendAsync("RefreshedAirQuality", stationState);
        }


    }
}