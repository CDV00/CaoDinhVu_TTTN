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
        Task<BaseResponse> AddAsync(ProductRequest productRequest);
        Task<BaseResponse> ChangeStatus(Guid id);
        Task<BaseResponse> Delete(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<PagingResponse<ListProductDTO>> Filter(FilterRequest filterRequest);
        Task<PagingResponse<ListProductDTO>> GetAll(PagingRequest pagingRequest);
        Task<List<ListProductDTO>> GetAll();
        Task<PagingResponse<ListProductDTO>> GetByBrandId(PagingRequest pagingRequest);
        Task<PagingResponse<ListProductDTO>> GetByCategoryId(PagingRequest pagingRequest);
        Task<ProductDTO> GetById(Guid id);
        Task<PagingResponse<ListProductDTO>> GetByKeyword(PagingRequest pagingRequest, string keyWork);
        Task<ProductCartItem> GetCartById(Guid productId, Guid optionId, Guid colorId);
        Task<BaseResponse> Update(ProductRequest productRequest);
    }
}
