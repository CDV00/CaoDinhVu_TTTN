using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IProductSevice
    {
        Task<PagingResponse<ListProductDTO>> Filter(FilterRequest filterRequest);
        Task<PagingResponse<ListProductDTO>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<ListProductDTO>> GetByBrandId(PagingRequest pagingRequest);
        Task<PagingResponse<ListProductDTO>> GetByCategoryId(PagingRequest pagingRequest);
        Task<ProductDTO> GetById(Guid id);
        Task<PagingResponse<ListProductDTO>> GetByKeyword(PagingRequest pagingRequest, string keyWork);
        Task<ProductCartItem> GetCartById(Guid productId, Guid optionId, Guid colorId);
    }
}
