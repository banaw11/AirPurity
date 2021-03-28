using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class StationStateDTO
    {
        private string _stCalcDate;
        public int Id { get; set; }
        public string StCalcDate 
        {
            get { return _stCalcDate; }
            set 
            {
                _stCalcDate = value;
                
                CalcDate = DateTime.ParseExact
               (
                  value,
                  "yyyy-MM-dd HH:mm:ss",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None
               );
            }
        }
        public DateTime CalcDate { get; set; }
        public IndexLevelDTO StIndexLevel { get; set; }

    }
}
