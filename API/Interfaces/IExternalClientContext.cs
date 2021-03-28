﻿using API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IExternalClientContext
    {
        Task<ICollection<StationDTO>> GetStationsAsync();
        Task<ICollection<SensorDTO>> GetSensorsAsync(int stationId);
        Task<ICollection<MeasureDTO>> GetMeasures(int sensorId);
        Task<StationStateDTO> GetStationState(int stationId);
    }
}
