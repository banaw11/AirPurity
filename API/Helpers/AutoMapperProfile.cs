using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.Data.Entities;
using AirPurity.API.DTOs.ClientDTOs;
using API.DTOs.ClientDTOs;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Station, StationExternal>()
                .ReverseMap()
                .ForMember(x => x.City, opt => opt.Ignore());

            CreateMap<City, CityExternal>()
                .ReverseMap()
                .ForMember(x => x.Commune, opt => opt.Ignore())
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.Name.ToUpper()));

            CreateMap<Commune, CommuneExternal>()
                .ForMember(c => c.CommuneName, opt => opt.MapFrom(c => c.CommuneName.ToUpper()))
                .ReverseMap()
                .ForMember(c => c.CommuneName, opt => opt.MapFrom(c => c.CommuneName.ToUpper()));

            CreateMap<SensorExternal, SensorData>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.ParamCode, opt => opt.MapFrom(x => x.Param.ParamCode))
                .ForMember(x => x.ParamName, opt => opt.MapFrom(x => x.Param.ParamName));

            CreateMap<City, CityClientDTO>();
            CreateMap<Station, StationClientDTO>();
            CreateMap<Province, ProvinceFormDTO>();
            CreateMap<District, DistrictFormDTO>();
            CreateMap<Commune, CommuneFormDTO>();
            CreateMap<City, CityFormDTO>();
            CreateMap<Notification, NotificationDTO>()
                .ReverseMap();
            CreateMap<NotificationSubject, NotificationSubjectDTO>()
                .ReverseMap();
                
        }
    }
}
