using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.BusinessLogic.Repositories.Repositories
{
    public class CommuneRepository : Repository<Commune>
    {
        public CommuneRepository(DataContext context) : base(context)
        {
        }
    }
}
