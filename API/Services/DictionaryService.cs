using AirPurity.API.BusinessLogic.Repositories.Repositories;
using AirPurity.API.DTOs;
using AirPurity.API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AirPurity.API.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly ProvinceRepository _provinceRepository;
        private readonly DistrictRepository _districtRepository;
        private readonly CommuneRepository _communeRepository;
        private readonly CityRepository _cityRepository;

        public DictionaryService(ProvinceRepository provinceRepository,
            DistrictRepository districtRepository,
            CommuneRepository communeRepository,
            CityRepository cityRepository)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _communeRepository = communeRepository;
            _cityRepository = cityRepository;
        }

        public IEnumerable<DictionaryModel> GetAllProvinces()
        {
            var entities = _provinceRepository.GetAll()
                .Select(x => new DictionaryModel(x.Id, x.ProvinceName));
            return entities;
        }

        public IEnumerable<DictionaryModel> GetCitiesByCommune(int communeId)
        {
            var entities = _cityRepository.FindAll(x => x.CommuneId == communeId)
                .Select(x => new DictionaryModel(x.Id, x.Name));
            return entities;
        }

        public IEnumerable<DictionaryModel> GetCommunesByDisctrict(int districtId)
        {
            var entities = _communeRepository.FindAll(x => x.DistrictId == districtId)
                .Select(x => new DictionaryModel(x.Id, x.CommuneName));
            return entities;
        }

        public IEnumerable<DictionaryModel> GetDistrictsByProvince(int provinceId)
        {
            var entities = _districtRepository.FindAll(x => x.ProvinceId == provinceId)
                .Select(x => new DictionaryModel(x.Id, x.DistrictName));
            return entities;
        }
    }
}
