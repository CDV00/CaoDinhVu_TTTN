using CaoDinhVu.DAL.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class ProductQuery : QueryBase<Product>, IProductQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public ProductQuery(IQueryable<Product> productQuery, DBContext dbContext) : base(productQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IProductQuery FiterProductColor(Guid productId)
        {
            Query = Query.Where(type => type.ProductColors.Any(pc => pc.Product.Id == productId));
            return this;
        }
        public IProductQuery IncludeProductColor()
        {
            Query.Include(p => p.ProductColors).Load();
            return this;
        }
    }
}
