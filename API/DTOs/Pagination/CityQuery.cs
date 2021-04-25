namespace API.DTOs.Pagination
{
    public class CityQuery
    {
        private string _provinceName;
        private string _districtName;
        private string _communeName;
        public string ProvinceName
        {
            get { return _provinceName; } 
            set { _provinceName = value == null? value : value.ToUpper(); } 
        }
        public string DistrictName 
        { 
            get{ return _districtName;} 
            set { _districtName =  value == null? value : value.ToUpper(); } 
        }
        public string CommuneName 
        { 
            get { return _communeName; } 
            set { _communeName =  value == null? value : value.ToUpper(); } 
        }
    }
}