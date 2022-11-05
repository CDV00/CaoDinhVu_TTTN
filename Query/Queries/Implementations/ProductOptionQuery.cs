using CaoDinhVu.DAL.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class ProductOptionQuery : QueryBase<ProductOption>, IProductOptionQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public ProductOptionQuery(IQueryable<ProductOption> productOptionQuery, DBContext dbContext) : base(productOptionQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IProductOptionQuery FilterByProductColorId(Guid id)
        {
            Query = Query.Where(po => po.ProductColor.Id == id);
            return this;
        }

        public List<Guid> FilterProductId(Guid productId)
        {
            var Ids = Query.Where(pc => pc.Product.Id == productId).Select(p => p.Id).ToList();

            return Ids;
        }

        public IProductOptionQuery IncludeOption()
        {
            Query.Include(po => po.Option).Load();
            return this;
        }
        public IProductOptionQuery IncludeColor()
        {
            Query.Include(po => po.ProductColor.Color).Load();
            return this;
        }
        public IProductOptionQuery IncludeProduct()
        {
            Query.Include(po => po.Product).Load();
            return this;
        }
        public IProductOptionQuery IncludeProductColor()
        {
            Query.Include(po => po.ProductColor).Load();
            return this;
        }
        public IProductOptionQuery FilterOption(Guid id)
        {
            Query = Query.Where(po => po.Option.Id == id);
            return this;
        }

    }
}
