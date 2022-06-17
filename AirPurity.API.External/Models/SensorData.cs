namespace AirPurity.API.BusinessLogic.External.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public string ParamName { get; set; }
        public string ParamCode { get; set; }
        public ICollection<Measure> Values { get; set; }
    }
}
