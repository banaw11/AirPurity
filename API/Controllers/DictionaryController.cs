﻿using AirPurity.API.DTOs;
using AirPurity.API.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AirPurity.API.Controllers
{
    public class DictionaryController : BaseApiController
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpGet("address/get-provinces")]
        [ResponseCache(Duration = 1800)]
        public IActionResult GetProvinces()
        {
            var provinces = _dictionaryService.GetAllProvinces();

            var responseModel = new ResponseModel(data: provinces);

            return Ok(responseModel);
        }

        [HttpGet("address/get-districts")]
        [ResponseCache(Duration = 1800, VaryByQueryKeys = new string[] { "provinceId"})]
        public IActionResult GetDistricts(int provinceId)
        {
            var districts = _dictionaryService.GetDistrictsByProvince(provinceId);

            var responseModel = new ResponseModel(data: districts);

            return Ok(responseModel);
        }

        [HttpGet("address/get-communes")]
        [ResponseCache(Duration = 1800, VaryByQueryKeys = new string[] { "districtId" })]
        public IActionResult GetComunes(int districtId)
        {
            var communnes = _dictionaryService.GetCommunesByDisctrict(districtId);

            var responseModel = new ResponseModel(data: communnes);

            return Ok(responseModel);
        }

        [HttpGet("address/get-cities")]
        [ResponseCache(Duration = 1800, VaryByQueryKeys = new string[] { "communeId" })]
        public IActionResult GetCities(int communeId)
        {
            var cities = _dictionaryService.GetCitiesByCommune(communeId);

            var responseModel = new ResponseModel(data: cities);

            return Ok(responseModel);
        }

        [HttpGet("index-levels")]
        public IActionResult GetIndexLevels()
        {
            var levels = _dictionaryService.GetIndexLevels();

            var responseModel = new ResponseModel(data: levels);

            return Ok(responseModel);
        }
    }
}
