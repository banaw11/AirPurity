using AirPurity.API.DTOs;
using System.Collections.Generic;

namespace AirPurity.API.Interfaces
{
    public interface IDictionaryService
    {
        IEnumerable<DictionaryModel> GetAllProvinces();
        IEnumerable<DictionaryModel> GetDistrictsByProvince(int provinceId);
        IEnumerable<DictionaryModel> GetCommunesByDisctrict(int districtId);
        IEnumerable<DictionaryModel> GetCitiesByCommune(int communeId);
        IEnumerable<DictionaryModel> GetIndexLevels();
    }
}
