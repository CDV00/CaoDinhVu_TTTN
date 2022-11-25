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
    public class ProductColorRepository : Repository<ProductColor>, IProductColorRepository
    {
        private readonly DBContext _context;
        public ProductColorRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IProductColorQuery BuildQuery()
        {
            return new ProductColorQuery(_context.ProductColors.AsQueryable(), _context);
        }
    }
}
