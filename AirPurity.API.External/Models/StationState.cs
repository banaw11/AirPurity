using System.Globalization;

namespace AirPurity.API.BusinessLogic.External.Models
{
    public class StationState
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
        public IndexLevel StIndexLevel { get; set; }
        public IndexLevel So2IndexLevel { get; set; }
        public IndexLevel No2IndexLevel { get; set; }
        public IndexLevel Pm10IndexLevel { get; set; }
        public IndexLevel Pm25IndexLevel { get; set; }
        public IndexLevel O3IndexLevel { get; set; }

    }
}
