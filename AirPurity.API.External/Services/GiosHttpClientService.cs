using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.Common.Exceptions;

namespace AirPurity.API.BusinessLogic.External.Services
{
    public class GiosHttpClientService
    {
        private readonly GiosHttpClientContext _context;

        public GiosHttpClientService(GiosHttpClientContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Measure>> GetMeasures(int sensorId)
        {
            var measureData = await _context.Get<MeasureData>(GiosHttpRequest.GetMeasures, sensorId);
            if (measureData is null) throw new NotFoundException($"Not found data for sensorId [{sensorId}]");

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

        public async Task<ICollection<SensorExternal>> GetSensorsAsync(int stationId)
        {
            var sensorsDTO = await _context.Get<ICollection<SensorExternal>>(GiosHttpRequest.GetSensors, stationId);
            if (!sensorsDTO.Any()) throw new NotFoundException($"Not found sensors for stationId [{stationId}]");

            return sensorsDTO;
        }

        public async Task<ICollection<StationExternal>> GetStationsAsync()
        {
            var stationsDTO = await _context.Get<ICollection<StationExternal>>(GiosHttpRequest.GetStations);
            if (!stationsDTO.Any()) throw new NotFoundException("Not found any stations from external API");

            return stationsDTO;

        }

        public async Task<StationState> GetStationState(int stationId)
        {
            var stationStateDTO = await _context.Get<StationState>(GiosHttpRequest.GetStationState,  stationId);
            if (stationStateDTO is null) throw new NotFoundException($"Not found data for stationId [{stationId}]");

            return stationStateDTO;
        }
    }
}
