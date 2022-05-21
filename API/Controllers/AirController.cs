using AirPurity.API.Interfaces;
using API.DTOs.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AirController : BaseApiController
    {
        private readonly IStationService _stationService;
        private readonly ISensorService _sensorService;

        public AirController(IStationService stationService, ISensorService sensorService)
        {
            _stationService = stationService;
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirData([FromQuery] SensorsDataQuery query)
        {

            var sensorsData = await _sensorService.GetSensorsDataAsync(query);

            return Ok(sensorsData);
            
        }
        
        [HttpGet("quality")]
        public async Task<IActionResult> GetAirQuality([FromQuery] int stationId)
        {
            var stationState = await _stationService.GetStationStateAsync(stationId);

            return Ok(stationState);
        }

        [HttpGet("norms")]
        [ResponseCache(Duration = 1800)]
        public IActionResult GetNorms()
        {
            var norms = _sensorService.GetNorms();
            
            return Ok(norms);
        }
    }
}
