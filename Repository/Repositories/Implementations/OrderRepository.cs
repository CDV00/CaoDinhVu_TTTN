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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DBContext _context;
        public OrderRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IOrderQuery BuildQuery()
        {
            return new OrderQuery(_context.Orders.AsQueryable(), _context);
        }
    }
}
