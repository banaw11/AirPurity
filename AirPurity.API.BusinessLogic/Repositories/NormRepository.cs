using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.BusinessLogic.Repositories
{
    public class NormRepository : Repository<Norm>
    {
        public NormRepository(DataContext context) : base(context)
        {
        }
    }
}
