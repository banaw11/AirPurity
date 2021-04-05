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

        public OnlineClientHub(OnlineTracker onlineTracker)
        {
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

    }
}