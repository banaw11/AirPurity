using API.Data;
using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityClientDTO> GetCityByNameAsync(string cityName)
        {
            var city = await _context.Cities.Where(x => x.Name.ToLower() == cityName.ToLower())
                .FirstOrDefaultAsync();

            return _mapper.Map<CityClientDTO>(city);
        }
    }
}
