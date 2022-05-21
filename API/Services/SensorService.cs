using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.BusinessLogic.External.Services;
using AirPurity.API.BusinessLogic.Repositories;
using AirPurity.API.Common.Exceptions;
using AirPurity.API.Data.Entities;
using AirPurity.API.Interfaces;
using API.DTOs.Pagination;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPurity.API.Services
{
    public class SensorService : ISensorService
    {
        private readonly NormRepository _normRepository;
        private readonly GiosHttpClientService _giosContext;
        private readonly IMapper _mapper;

        public SensorService(NormRepository normRepository, GiosHttpClientService giosContext, IMapper mapper)
        {
            _normRepository = normRepository;
            _giosContext = giosContext;
            _mapper = mapper;
        }

        public ICollection<Norm> GetNorms()
        {
            return _normRepository.GetAll();
        }

        public async Task<ICollection<SensorExternal>> GetSensors(int stationId)
        {
            var sensors = await _giosContext.GetSensorsAsync(stationId);
            if (!sensors.Any()) throw new NotFoundException($"Sensors for stationID [{stationId}] not found");

            return sensors;
        }

        public async Task<ICollection<SensorData>> GetSensorsDataAsync(SensorsDataQuery query)
        {
            var sensorsData = _mapper.Map<ICollection<SensorData>>(await GetSensors(query.StationId));

            foreach (var sensorData in sensorsData)
            {
                var values = await _giosContext.GetMeasures(sensorData.Id);

                sensorData.Values = new List<Measure>();
                if (query.Range == RangeOfData.LATEST)
                    sensorData.Values.Add(values.FirstOrDefault());
                else
                    sensorData.Values = values;
            }

            var sensorsDataDTO = sensorsData.Where(s => s.Values.Count() > 0).ToList();

            return sensorsDataDTO;
        }
    }
}
