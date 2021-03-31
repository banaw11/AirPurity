using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using System.Text.Json;
using API.Interfaces;
using API.DTOs;
using System.Linq;

namespace API.Repositories
{
    public class ExternalClientContext : IExternalClientContext
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;

        public ExternalClientContext(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("gios");
        }

        public async Task<ICollection<MeasureDTO>> GetMeasures(int sensorId)
        {
            var measureData = await _client.GetFromJsonAsync<MeasureDataDTO>("data/getData/" + sensorId);
            DateTime currentDate = DateTime.UtcNow.Date.AddDays(-1);
            return measureData.Values.Where(x => x.Value != null && x.DateFormat >= currentDate).ToList();
        }

        public async Task<ICollection<SensorDTO>> GetSensorsAsync(int stationId)
        {
            return await _client.GetFromJsonAsync<ICollection<SensorDTO>>("station/sensors/" + stationId);
        }

        public async Task<ICollection<StationDTO>> GetStationsAsync()
        {

            return await _client.GetFromJsonAsync<ICollection<StationDTO>>("station/findAll");
            
        }

        public async Task<StationStateDTO> GetStationState(int stationId)
        {
            return await _client.GetFromJsonAsync<StationStateDTO>("aqindex/getIndex/" + stationId);
        }
    }
}
