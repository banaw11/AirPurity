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

        public override ICollection<Province> GetAll()
        {
            return _context.Provinces
                .Include(p => p.Districts)
                .ThenInclude(d => d.Communes)
                .ThenInclude(c => c.Cities)
                .ToList();
        }

        public override ICollection<Province> FindAll(Expression<Func<Province, bool>> expression)
        {
            return _context.Provinces.Where(expression)
                .Include(p => p.Districts)
                .ThenInclude(d => d.Communes)
                .ThenInclude(c => c.Cities)
                .ToList();
        }
    }
}
