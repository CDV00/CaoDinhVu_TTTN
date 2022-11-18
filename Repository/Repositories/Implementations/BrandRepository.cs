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

        public void ChangeOrder(int order, int? orderOld = 0)
        {
            //thằng đang giữ order
            var brand = _context.Brands.Where(c => c.Orders == order).FirstOrDefault();
            if (brand is not null)
            {
                if (orderOld > 0)
                {
                    //thằng muốn chiếm order
                    var brandOld = _context.Brands.Where(c => c.Orders == orderOld).FirstOrDefault();
                    //gán 
                    brand.Orders = orderOld;
                    brandOld.Orders = order;
                    _context.SaveChanges();
                    //break;
                }
                else
                {
                    int orderMax = _context.Brands.Max(c => c.Orders).Value;
                    brand.Orders = ++orderMax;
                    _context.SaveChanges();
                }
            }
        }

        public bool CheckExists(Guid id) =>_context.Brands.Any(b => b.Id == id);

    }
}
