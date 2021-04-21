using API.Data;
using API.DTOs.ClientDTOs;
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

        public async Task<ICollection<ProvinceFormDTO>> GetCitiesAsync()
        {
            var provinces = new List<ProvinceFormDTO>();
            // var results = _context.Communes.Select(x => x).AsEnumerable().GroupBy(x => x.ProvinceName);

            // foreach(var result in results)
            // {
            //     var province = new ProvinceFormDTO();
            //     province.Name = result.Key;
            //     province.Districts = new List<DistrictFormDTO>();
            //     var districtsResults = result.Select(x => x).AsEnumerable().GroupBy(x => x.DistrictName);
            //     foreach (var districtResult in districtsResults)
            //     {
            //         var district = new DistrictFormDTO();
            //         district.Name = districtResult.Key;
            //         district.Communes = new List<CommuneFormDTO>();
            //         var communesResults = districtResult.Select(x => x).AsEnumerable().GroupBy(x => x.CommuneName);
            //         foreach (var communeResult in communesResults)
            //         {
            //             var commune = new CommuneFormDTO();
            //             commune.Name = communeResult.Key;
            //             commune.Cities = await _context.Cities
            //                 .Where(x => x.CommuneName == commune.Name && x.DistrictName == district.Name)
            //                 .Select(x => new CityFormDTO{Name = x.Name, Id = x.Id})
            //                 .ToListAsync();

            //             district.Communes.Add(commune);
                        
            //         }
            //         province.Districts.Add(district);
            //     }
            //     provinces.Add(province);
            // }

            return provinces;
        }

        public async Task<CityClientDTO> GetCityByNameAsync(string cityName)
        {
            var city = await _context.Cities.Where(x => x.Name.ToLower() == cityName.ToLower())
                .FirstOrDefaultAsync();

            return _mapper.Map<CityClientDTO>(city);
        }
    }
}
