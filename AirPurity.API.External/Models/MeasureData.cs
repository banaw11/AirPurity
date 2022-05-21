namespace AirPurity.API.BusinessLogic.External.Models
{
    public class MeasureData
    {
        public string Key { get; set; }
        public ICollection<Measure> Values { get; set; }
    }
}
