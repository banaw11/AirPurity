using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class OnlineTracker
    {
        private static readonly Dictionary<string, int> OnlineClients = new Dictionary<string, int>();

        public Task ClientConnected(string connectionId, int stationId)
        {
            lock(OnlineClients)
            {
                if(OnlineClients.ContainsKey(connectionId))
                    OnlineClients[connectionId]=stationId;
                else OnlineClients.Add(connectionId, stationId);
            }

            return Task.CompletedTask;
        }

        public Task ClientDisconnected(string connectionId)
        {
            lock (OnlineClients)
            {
                if(OnlineClients.ContainsKey(connectionId))
                    OnlineClients.Remove(connectionId);
            }

            return Task.CompletedTask;
        }
    }
}