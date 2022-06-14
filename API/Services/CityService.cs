using AirPurity.API.BusinessLogic.Repositories.Repositories;
using AirPurity.API.Interfaces;
using API.DTOs.ClientDTOs;
using AutoMapper;
using System.Linq;

namespace AirPurity.API.Services
{
    public class CityService : ICityService
    {
        private readonly CityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(CityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public CityClientDTO GetCityByName(string name)
        {
            var city = _cityRepository
                .FindAll(x => x.Name == name, x => x.Stations)
                .FirstOrDefault();

            if(city != null)
            {
                return _mapper.Map<CityClientDTO>(city);
            }

            return null;
        }
    }
}
