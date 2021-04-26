using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.DTOs;
using System.Linq;
using API.Middleware.Exceptions;

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
            if(measureData is null) throw new NotFoundException($"Not found data for sensorId [{sensorId}]");

            DateTime currentDate = DateTime.UtcNow.ToUniversalTime().AddHours(2);
            var lastMeasureDate = measureData.Values.Count > 0 ?
                measureData.Values
                    .OrderByDescending(m => m.DateFormat)
                    .Select(m => m.DateFormat)
                    .FirstOrDefault() :
                currentDate;

            lastMeasureDate = lastMeasureDate.AddDays(-1);

            var measuresDTO = measureData.Values.Where(x => x.Value != null &&
                 x.DateFormat >= lastMeasureDate).ToList();

            return measuresDTO;
        }

        public async Task<ICollection<SensorDTO>> GetSensorsAsync(int stationId)
        {
            var sensorsDTO = await _client.GetFromJsonAsync<ICollection<SensorDTO>>("station/sensors/" + stationId);
            if(!sensorsDTO.Any()) throw new NotFoundException($"Not found sensors for stationId [{stationId}]");

            return sensorsDTO;
        }

        public async Task<ICollection<StationDTO>> GetStationsAsync()
        {
            var stationsDTO = await _client.GetFromJsonAsync<ICollection<StationDTO>>("station/findAll");
            if(!stationsDTO.Any()) throw new NotFoundException("Not found any stations from external API");
            
            return stationsDTO;
            
        }

        public async Task<StationStateDTO> GetStationState(int stationId)
        {
            var stationStateDTO = await _client.GetFromJsonAsync<StationStateDTO>("aqindex/getIndex/" + stationId);
            if(stationStateDTO is null) throw new NotFoundException($"Not found data for stationId [{stationId}]");

            return stationStateDTO;
        }
    }
}
