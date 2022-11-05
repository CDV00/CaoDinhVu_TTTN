using CaoDinhVu.DAL.Data;
using Entities.Constants;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class ProductColorQuery : QueryBase<ProductColor>, IProductColorQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public ProductColorQuery(IQueryable<ProductColor> productColorQuery, DBContext dbContext) : base(productColorQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Guid FilterProductColorId(Guid productId, Guid colorId)
        {
            Guid productColorId = Query.Where(pc => pc.Product.Id == productId && pc.Color.Id == colorId).FirstOrDefault().Id;

            return productColorId;
        }
        public List<Guid> FilterProductId(Guid productId)
        {
            var Ids = Query.Where(pc => pc.Product.Id == productId).Select(p=>p.Id).ToList();

            return Ids;
        }
    }
}
