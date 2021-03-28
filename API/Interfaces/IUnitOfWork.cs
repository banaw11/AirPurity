using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IStationRepository StationRepository { get; }
        ISensorRepository SensorRepository { get; }
        ICityRepository CityRepository { get; }
    }
}
