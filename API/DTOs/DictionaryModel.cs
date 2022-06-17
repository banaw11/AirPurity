namespace AirPurity.API.DTOs
{
    public class DictionaryModel
    {
        public DictionaryModel(dynamic value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public dynamic Value { get; set; }
        public string Name { get; set; }
    }
}
