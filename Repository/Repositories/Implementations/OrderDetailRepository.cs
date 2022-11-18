using AutoMapper;
using CaoDinhVu.DAL.Data;
using Entities.DTOs;
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
        private readonly IMapper _mapper;

        public OrderDetailRepository(DBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IOrderDetailQuery BuildQuery()
        {
            return new OrderDetailQuery(_context.OrderDetails.AsQueryable(), _context);
        }

        public Task<OrderDetailDTO> GetAllOrderdetail()
        {
            try
            {
                var Result = from od in _context.OrderDetails
                             join p in _context.Products
                             on od.ProductId equals p.Id
                             join pc in _context.ProductColors
                             on od.ProductColorId equals pc.Id
                             join po in _context.ProductOptions
                             on od.productOptionId equals po.Id
                             select new
                             {
                                 OrderDetailDTO = _mapper.Map<OrderDetailDTO>(od)
                             };
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
