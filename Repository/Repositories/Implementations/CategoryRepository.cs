using CaoDinhVu.DAL.Data;
using Entities.Models;
using System;
using Query.Queries;
using Query.Queries.Implementations;
using System.Linq;
using System.Threading.Tasks;

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

        public void ChangeOrder(int order, int? orderOld = 0)
        {
            //thằng đang giữ order
            var category = _context.Categories.Where(c => c.Orders == order).FirstOrDefault();
            if(category is not null)
            {
                if (orderOld > 0)
                {
                    //thằng muốn chiếm order
                    var categoryOld = _context.Categories.Where(c => c.Orders == orderOld).FirstOrDefault();
                    //gán 
                    category.Orders = orderOld;
                    categoryOld.Orders = order;
                    _context.SaveChanges();
                    //break;
                }
                else
                {
                    int orderMax = _context.Categories.Max(c=>c.Orders).Value;
                    category.Orders = ++orderMax;
                    _context.SaveChanges();
                }
            }
        }
        public bool CheckExists(Guid id) => _context.Brands.Any(b => b.Id == id);
    }
}
