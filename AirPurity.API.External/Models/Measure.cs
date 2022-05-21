using System.Globalization;

namespace AirPurity.API.BusinessLogic.External.Models
{
    public class Measure
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
