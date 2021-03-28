using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ICollection<SensorDataDTO>> GetAirData([FromQuery] int stationId)
        {

            var sensorsData = await _unitOfWork.SensorRepository.GetSensorsData(stationId);

            return await Task.FromResult(sensorsData);
            
        }

        [HttpGet("quality")]
        public async Task<StationStateDTO> GetAirQuality([FromQuery] int stationId)
        {
            var stationState = await _unitOfWork.StationRepository.GetStationState(stationId);

            return await Task.FromResult(stationState);
        }
    }
}
