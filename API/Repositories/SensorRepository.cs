using API.DTOs;
using API.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly IExternalClientContext _clientContext;
        private readonly IMapper _mapper;

        public SensorRepository(IExternalClientContext clientContext, IMapper mapper)
        {
            _clientContext = clientContext;
            _mapper = mapper;
        }

        public async Task<ICollection<SensorDTO>> GetSensors(int stationId)
        {
            return await _clientContext.GetSensorsAsync(stationId);
        }

        public async Task<ICollection<SensorDataDTO>> GetSensorsData(int stationId)
        {
            var sensorsData = _mapper.Map<ICollection<SensorDataDTO>>(await GetSensors(stationId));

            foreach (var sensorData in sensorsData)
            {
                var values = await _clientContext.GetMeasures(sensorData.Id);
                sensorData.Values = values.Where(x => x.Value != null)
                    .OrderByDescending(x => x.DateFormat)
                    .ToList();
            }

            return sensorsData;
        }
    }
}
