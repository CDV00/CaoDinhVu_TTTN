using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface IProductQuery : IQuery<Product>
    {
        IProductQuery FilterByColorId(Guid? colorId);
        IProductQuery FilterByKeyword(string keyWork);
        IProductQuery FilterByOptionId(Guid? optionId);
        IProductQuery FilterBrandId(Guid? id);
        IProductQuery FilterCategoryId(Guid? id);
        IProductQuery FilterProductId(Guid? productId);
        IProductQuery IncludeBrand();
        IProductQuery IncludeCategory();
        IProductQuery IncludeColor();
        IProductQuery IncludeDetail();
        IProductQuery IncludeImage();
        IProductQuery IncludeOption();
        IProductQuery IncludeProductColor();
        IProductQuery IncludeProductOption();
        IProductQuery FilterByPriceMax(decimal? price);
        IProductQuery FilterByPriceMin(decimal? price);
    }
}
