using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly DBContext _context;
        public ColorRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IColorQuery BuildQuery()
        {
            return new ColorQuery(_context.Colors.AsQueryable(), _context);
        }
        public bool CheckExist(string Name)
        {
            var result = _context.Colors.Any(c => c.Name == Name);
            return result;
        }
    }
}
