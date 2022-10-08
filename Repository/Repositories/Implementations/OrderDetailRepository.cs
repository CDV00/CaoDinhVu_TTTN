﻿using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private DBContext _context;
        public OrderDetailRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IOrderDetailQuery BuildQuery()
        {
            return new OrderDetailQuery(_context.OrderDetails.AsQueryable(), _context);
        }
    }
}