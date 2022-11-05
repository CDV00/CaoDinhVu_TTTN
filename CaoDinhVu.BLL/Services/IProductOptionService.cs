using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IProductOptionService
    {
        Task<BaseResponse> AddAsync(ProductOptionRequest productOptionRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<ProductOptionCartItem> GetByProductColor(Guid productColorId, Guid optionId);
        List<Guid> GetIdByProductId(Guid productId);
        Task<List<OptionDTO>> getOptionbyProductcolorId(Guid productColorId);
        Task<decimal> GetPrice(Guid productColorId, Guid optionId);
        Task<BaseResponse> Update(ProductOptionRequest productOptionRequest);
    }
}
