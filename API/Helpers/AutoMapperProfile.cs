using API.DTOs;
using API.DTOs.ClientDTOs;
using API.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Station, StationDTO>()
                .ReverseMap()
                .ForMember(x => x.City, opt => opt.Ignore());

            CreateMap<City, CityDTO>()
                .ReverseMap()
                .ForMember(x => x.Commune, opt => opt.Ignore())
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.Name.ToUpper()));

            CreateMap<Commune, CommuneDTO>()
                .ForMember(c => c.CommuneName, opt => opt.MapFrom(c => c.CommuneName.ToUpper()))
                .ReverseMap()
                .ForMember(c => c.CommuneName, opt => opt.MapFrom(c => c.CommuneName.ToUpper()));

            CreateMap<SensorDTO, SensorDataDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.ParamCode, opt => opt.MapFrom(x => x.Param.ParamCode))
                .ForMember(x => x.ParamName, opt => opt.MapFrom(x => x.Param.ParamName))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<City, CityClientDTO>();
            CreateMap<Station, StationClientDTO>();
            CreateMap<Province, ProvinceFormDTO>();
            CreateMap<District, DistrictFormDTO>();
            CreateMap<Commune, CommuneFormDTO>();
            CreateMap<City, CityFormDTO>();
                
        }
    }
}
