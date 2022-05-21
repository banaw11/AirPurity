using API.DTOs.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class CityController : BaseApiController
    {
        public CityController()
        {
        }

        [HttpGet("All")]
        [ResponseCache(Duration = 1800, VaryByQueryKeys = new []{"provinceName","districtName","communeName"})]
        public IActionResult GetCitiesAsync([FromQuery] CityQuery query)
        {
            //var proviences = await _unitOfWork.CityRepository.GetCitiesAsync(query);
            
            //return Ok(proviences);

            throw new NotImplementedException();
        }

        [HttpGet]
        [ResponseCache(Duration = 300, VaryByQueryKeys = new []{"cityName"})]
        public IActionResult GetCityAsync([FromQuery] string cityName)
        {
            //var city = await _unitOfWork.CityRepository.GetCityByNameAsync(cityName);

            //return city;

            throw new NotImplementedException();
        }
    }
}
