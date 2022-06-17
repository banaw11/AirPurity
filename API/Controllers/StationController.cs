using AirPurity.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class StationController : BaseApiController
    {
        private readonly IStationService _stationService;
        private readonly ISensorService _sensorService;

        public StationController(IStationService stationService, ISensorService sensorService)
        {
            _stationService = stationService;
            _sensorService = sensorService;
        }
        
        [HttpGet("sensors")]
        public async Task<IActionResult> GetSensorsAsync([FromQuery] int stationId)
        {
            var sensors = await _sensorService.GetSensors(stationId);

            return Ok(sensors);
        }
        
        [HttpGet]
        public IActionResult GetStation([FromQuery] int stationId)
        {
            var station = _stationService.GetStationsById(stationId);

            return Ok(station);
        }
    }
}
