using API.Data;
using API.DTOs.ClientDTOs;
using API.DTOs.Pagination;
using API.Interfaces;
using API.Middleware.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ProvinceFormDTO>> GetCitiesAsync(CityQuery query)
        {
            var provinces = await _context.Provinces
                .Include(p => p.Districts
                    .Where(d => query.ProvinceName != null &&
                        d.Province.ProvinceName == query.ProvinceName))
                .ThenInclude(d => d.Communes
                    .Where(c => query.DistrictName != null &&
                        c.District.DistrictName == query.DistrictName))
                .ThenInclude(c => c.Cities
                    .Where(c => query.CommuneName != null &&
                         c.Commune.CommuneName == query.CommuneName))
                .ToListAsync();


            var provincesDTO = _mapper.Map<IEnumerable<ProvinceFormDTO>>(provinces);
            return provincesDTO;
        }

        public async Task<CityClientDTO> GetCityByNameAsync(string cityName)
        {
            var city = await _context.Cities.Where(x => x.Name == cityName.ToUpper())
                .Include(c => c.Stations)
                .FirstOrDefaultAsync();

           if(city == null) throw new NotFoundException($"City [{cityName}] not found");

            return _mapper.Map<CityClientDTO>(city);
        }
    }
}
