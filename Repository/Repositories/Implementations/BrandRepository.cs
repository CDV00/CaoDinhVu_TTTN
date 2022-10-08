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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private DBContext _context;
        public BrandRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IBrandQuery BuildQuery()
        {
            return new BrandQuery(_context.Brands.AsQueryable(), _context);
        }
    }
}
