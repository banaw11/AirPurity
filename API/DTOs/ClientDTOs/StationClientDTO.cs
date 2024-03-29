﻿using AirPurity.API.BusinessLogic.External.Models;
using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class StationClientDTO
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public double GegrLat { get; set; }
        public double GegrLon { get; set; }
        public string AddressStreet { get; set; }
        public ICollection<SensorExternal> Sensors { get; set; }
    }
}
