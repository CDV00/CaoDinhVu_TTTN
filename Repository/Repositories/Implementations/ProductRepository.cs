using AutoMapper;
using CaoDinhVu.DAL.Data;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public ProductRepository(DBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IProductQuery BuildQuery()
        {
            return new ProductQuery(_context.Products.AsQueryable(), _context);
        }
        /*public Product GetById(Guid id)
        {
            var product = BuildQuery().FilterProductId(id);
            


            return new ProductQuery(_context.Products.AsQueryable(), _context);
        }*/



        public async Task<List<Product>> GetAllTest()
        {
            var query = from p in _context.Products
                        where p.Name != null
                        select _mapper.Map<ListProductDTO>(p);
                        ;
            var product = await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return product;
        }

    }
}
