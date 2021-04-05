using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public interface IHubRepository
    {
        Task RefreshStationData(ClientDto clientDto);
        ICollection<ClientDto> GetOnlineClients();
        void RefreshClientsData();
    }
}
