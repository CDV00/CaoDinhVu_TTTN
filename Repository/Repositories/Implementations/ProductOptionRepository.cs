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
    public class ProductOptionRepository : Repository<ProductOption>, IProductOptionRepository
    {
        private readonly DBContext _context;
        public ProductOptionRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IProductOptionQuery BuildQuery()
        {
            return new ProductOptionQuery(_context.ProductOptions.AsQueryable(), _context);
        }
    }
}
