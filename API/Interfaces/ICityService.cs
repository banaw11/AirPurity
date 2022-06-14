using API.DTOs.ClientDTOs;

namespace AirPurity.API.Interfaces
{
    public interface ICityService
    {
        CityClientDTO GetCityByName(string name);
    }
}
