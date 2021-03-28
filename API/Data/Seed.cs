using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedStations(IExternalClientContext clientContext, DataContext context, IMapper mapper)
        {
            if (await context.Stations.AnyAsync()) return;

            var stationsDTO = await clientContext.GetStationsAsync();
            var communesDTO = stationsDTO.Select(x =>  x.City.Commune)
                .GroupBy(x => new { x.CommuneName, x.DistrictName })
                .Select(x => x.First()).ToList();

            foreach (var communeDTO in communesDTO)
            {
                var commune = mapper.Map<Commune>(communeDTO);
                commune.Cities = stationsDTO.Where(x => x.City.Commune == communeDTO)
                    .Select(x => mapper.Map<City>(x.City))
                    .GroupBy(x => x.Id)
                    .Select(x => x.First()).ToList();
                foreach (var city in commune.Cities)
                {
                    city.Stations = stationsDTO.Where(x => x.City.Id == city.Id)
                        .Select(x => mapper.Map<Station>(x))
                        .ToList();
                }
                context.Add(commune);
            }

            await context.SaveChangesAsync();
        }
    }
}
