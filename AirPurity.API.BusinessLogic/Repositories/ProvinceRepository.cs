using AirPurity.API.Data.Entities;
using AirPurity.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirPurity.API.BusinessLogic.Repositories.Repositories
{
    public class ProvinceRepository : Repository<Province>
    {
        private readonly DataContext _context;

        public ProvinceRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
