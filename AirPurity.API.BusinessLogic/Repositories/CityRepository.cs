using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.BusinessLogic.Repositories
{
    public class CityRepository : Repository<City>
    {
        public CityRepository(DataContext context) : base(context)
        {
        }
    }
}
