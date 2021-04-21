using API.DTOs.ClientDTOs;
using API.DTOs.Pagination;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CityController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ProvinceFormDTO>>> GetCitiesAsync([FromQuery] CityQuery query)
        {
            var proviences = await _unitOfWork.CityRepository.GetCitiesAsync(query);
            if(proviences.Any())
                return Ok(proviences);
            return BadRequest("Failed during load data");
        }

        [HttpGet]
        public async Task<ActionResult<CityClientDTO>> GetCityAsync([FromQuery] string cityName)
        {
            var city = await _unitOfWork.CityRepository.GetCityByNameAsync(cityName);
            city.Stations = await _unitOfWork.StationRepository.GetStationsByCityAsync(cityName);
            if (city != null)
                return Ok(city);

            return BadRequest("City not found");
        }
    }
}
