using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.SignalR
{
    public interface IHubRepository
    {
        Task RefreshStationData(ClientDto clientDto);
        ICollection<ClientDto> GetOnlineClients();
        Task RefreshClientsData();
    }
}
