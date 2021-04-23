using API.DTOs.ClientDTOs;
using API.DTOs.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ICityRepository
    {
        Task<CityClientDTO> GetCityByNameAsync(string cityName);
        Task<IEnumerable<ProvinceFormDTO>> GetCitiesAsync(CityQuery query);
    }
}
