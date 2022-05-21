using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.BusinessLogic.Repositories
{
    public class StationRepository : Repository<Station>
    {
        public StationRepository(DataContext context) : base(context)
        {
        }

    }
}
