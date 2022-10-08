using CaoDinhVu.DAL.Data;
using Entities.Models;
using System;
using Query.Queries;
using Query.Queries.Implementations;

namespace Repository.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private DBContext _context;
        public CategoryRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public ICategoryQuery BuildQuery()
        {
            return new CategoryQuery(_context.Categories.AsQueryable(), _context);
        }
    }
}
