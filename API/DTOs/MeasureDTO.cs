using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MeasureDTO
    {
        private string _date;
        public DateTime DateFormat { get; set; }
        public string Date
        {
            get { return _date; } 
            set 
            {
                _date = value;
                DateFormat = DateTime.ParseExact
               (
                  value,
                  "yyyy-MM-dd HH:mm:ss",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None
               );
            } 
        }
        public double? Value { get; set; }
    }
}
