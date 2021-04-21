using API.Data;
using API.DTOs.ClientDTOs;
using API.DTOs.Pagination;
using API.Interfaces;
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
                .Where(p =>  p.ProvinceName == query.ProvinceName || 
                    query.ProvinceName == null)
                .Include(p => p.Districts
                    .Where(d => d.DistrictName == query.DistrictName ||
                        (query.DistrictName == null &&
                        query.ProvinceName != null)))
                .ThenInclude(d => d.Communes
                    .Where(c => c.CommuneName == query.CommuneName ||
                        (query.CommuneName == null  && 
                        query.ProvinceName != null && 
                        query.DistrictName != null)))
                .ThenInclude(c => c.Cities
                    .Where(c => query.ProvinceName != null && 
                        query.DistrictName != null && 
                        query.CommuneName != null))
                .ToListAsync();

            var provincesDTO = _mapper.Map<IEnumerable<ProvinceFormDTO>>(provinces);
            return provincesDTO;
        }

        public async Task<CityClientDTO> GetCityByNameAsync(string cityName)
        {
            var city = await _context.Cities.Where(x => x.Name.ToLower() == cityName.ToLower())
                .FirstOrDefaultAsync();

            return _mapper.Map<CityClientDTO>(city);
        }
    }
}
