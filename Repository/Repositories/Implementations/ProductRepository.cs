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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private DBContext _context;
        public ProductRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IProductQuery BuildQuery()
        {
            return new ProductQuery(_context.Products.AsQueryable(), _context);
        }
    }
}
