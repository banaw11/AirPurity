using AirPurity.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CityController : BaseApiController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new []{"cityName"})]
        public IActionResult GetCityAsync([FromQuery] string cityName)
        {
            var city = _cityService.GetCityByName(cityName);

            return Ok(city);

        }
    }
}
