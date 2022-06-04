using AirPurity.API.BusinessLogic.External.Services;
using AirPurity.API.Data.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AirPurity.API.Data
{
    public class Seed
    {
        public static async Task SeedStations(GiosHttpClientService clientContext, DataContext context, IMapper mapper)
        {
            if (await context.Stations.AnyAsync() && await context.Norms.AnyAsync()) return;

            if(await context.Stations.AnyAsync() == false)
            {
                var stationsDTO = await clientContext.GetStationsAsync();
                var communes = stationsDTO.Select(x => x.City.Commune)
                    .GroupBy(x => new { x.CommuneName, x.DistrictName, x.ProvinceName })
                    .Select(x => x.First()).ToList();
                var provinces = communes.Select(x => x)
                    .GroupBy(x => x.ProvinceName)
                    .Select(x => x.First()).OrderBy(x => x.ProvinceName).Select(x => x.ProvinceName.ToUpper()).ToList();
                var districts = communes.Select(x => x)
                    .GroupBy(x => new {x.DistrictName, x.ProvinceName})
                    .Select(x => x.First()).OrderBy(x => x.DistrictName).Select(x => new {x.DistrictName, x.ProvinceName}).ToList();
                var cities = stationsDTO.Select(x => x.City)
                    .GroupBy(x => new {x.Id, x.Commune.CommuneName})
                    .Select(x => x.First()).OrderBy(x => x.Name).Select(x => x).ToList();

                var provincesList = new List<Province>();
                foreach(var provinceDTO in provinces)
                {
                    var province = new Province()
                    {
                        Id = provinces.IndexOf(provinceDTO)+1,
                        ProvinceName = provinceDTO,
                        Districts = new List<District>()
                    };
                    foreach(var districtDTO in districts.Where(x => x.ProvinceName.ToUpper() == province.ProvinceName))
                    {
                        var district = new District()
                        {
                            Id = districts.IndexOf(districtDTO)+1,
                            DistrictName = districtDTO.DistrictName.ToUpper(),
                            ProvinceId = province.Id,
                            Communes = new List<Commune>()
                        };
                        foreach (var communeDTO in communes.Where(x => x.DistrictName.ToUpper() == district.DistrictName &&
                                                                        x.ProvinceName.ToUpper() == province.ProvinceName))
                        {
                            var commune = new Commune()
                            {
                                Id = communes.IndexOf(communeDTO)+1,
                                CommuneName = communeDTO.CommuneName.ToUpper(),
                                DistrictId = district.Id,
                                Cities = new List<City>()
                            };
                            
                            foreach (var cityDTO in cities.Where(x => x.Commune.CommuneName.ToUpper() == commune.CommuneName ))
                            {
                                var city = mapper.Map<City>(cityDTO);
                                city.Stations = stationsDTO.Where(x => x.City.Id == city.Id)
                                    .Select(x => mapper.Map<Station>(x))
                                    .ToList();
                                commune.Cities.Add(city);

                            }
                            district.Communes.Add(commune);
                        }   
                        province.Districts.Add(district);
                    }
                    provincesList.Add(province);
                }

               context.AddRange(provincesList);
            }

            if(await context.Stations.AnyAsync() == false)
            {
                var normsData = await System.IO.File.ReadAllTextAsync("../AirPurity.API.Data/NormsSeed.json");
                var norms = JsonSerializer.Deserialize<List<Norm>>(normsData);

                context.Norms.AddRange(norms);
            }
           

            await context.SaveChangesAsync();
        }
    }
}
