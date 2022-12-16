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
        IProductQuery FilterByKeyword(string? keyWork = null);
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
        IProductQuery IncludeProductColor(int status);
        IProductQuery IncludeProductOption(int status);
        IProductQuery FilterByPriceMax(decimal? price);
        IProductQuery FilterByPriceMin(decimal? price);
        IProductQuery FilterStatus(int status);
    }
}
