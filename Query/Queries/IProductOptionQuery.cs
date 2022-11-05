using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface IProductOptionQuery : IQuery<ProductOption>
    {
        IProductOptionQuery FilterByProductColorId(Guid id);
        IProductOptionQuery FilterOption(Guid id);
        List<Guid> FilterProductId(Guid productId);
        IProductOptionQuery IncludeColor();
        IProductOptionQuery IncludeOption();
        IProductOptionQuery IncludeProduct();
        IProductOptionQuery IncludeProductColor();
    }
}
