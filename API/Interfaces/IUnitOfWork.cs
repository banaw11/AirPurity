namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IStationRepository StationRepository { get; }
        ISensorRepository SensorRepository { get; }
        ICityRepository CityRepository { get; }
    }
}
