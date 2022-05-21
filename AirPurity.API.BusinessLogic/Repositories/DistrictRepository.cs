using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.BusinessLogic.Repositories.Repositories
{
    public class DistrictRepository : Repository<District>
    {
        public DistrictRepository(DataContext context) : base(context)
        {
        }
    }
}
