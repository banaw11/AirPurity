using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Middleware.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly DataContext _context;

        public SensorRepository(IExternalClientContext clientContext, IMapper mapper, DataContext context)
        {
            _clientContext = clientContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<Norm>> GetNormsAsync()
        {
            return await _context.Norms.ToListAsync();
        }

        public async Task<ICollection<SensorDTO>> GetSensors(int stationId)
        {
            var sensors = await _clientContext.GetSensorsAsync(stationId);
            if(!sensors.Any()) throw new NotFoundException($"Sensors for stationID [{stationId}] not found");

            return sensors;
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
