using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class StationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExternalClientContext _clientContext;

        public StationController(IUnitOfWork unitOfWork, IExternalClientContext clientContext)
        {
            _unitOfWork = unitOfWork;
            _clientContext = clientContext;
        }
        
        [HttpGet("sensors")]
        public async Task<ICollection<SensorDTO>> GetSensorsAsync([FromQuery] int stationId)
        {
            var sensors = await _clientContext.GetSensorsAsync(stationId);

            return await Task.FromResult(sensors);
        }
    }
}
