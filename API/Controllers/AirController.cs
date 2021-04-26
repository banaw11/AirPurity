using API.DTOs;
using API.DTOs.Pagination;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AirController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AirController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ICollection<SensorDataDTO>> GetAirData([FromQuery] SensorsDataQuery query)
        {

            var sensorsData = await _unitOfWork.SensorRepository.GetSensorsData(query);

            return await Task.FromResult(sensorsData);
            
        }
        
        [HttpGet("quality")]
        public async Task<StationStateDTO> GetAirQuality([FromQuery] int stationId)
        {
            var stationState = await _unitOfWork.StationRepository.GetStationState(stationId);

            return await Task.FromResult(stationState);
        }

        [HttpGet("norms")]
        [ResponseCache(Duration = 1800)]
        public async Task<ICollection<Norm>> GetNorms()
        {
            var norms = await _unitOfWork.SensorRepository.GetNormsAsync();
            
            return await Task.FromResult(norms);
        }
    }
}
