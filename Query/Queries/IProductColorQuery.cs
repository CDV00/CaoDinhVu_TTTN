using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface IProductColorQuery : IQuery<ProductColor>
    {
        Guid FilterProductColorId(Guid productId, Guid colorId);
        List<Guid> FilterProductId(Guid productId);
    }
}
