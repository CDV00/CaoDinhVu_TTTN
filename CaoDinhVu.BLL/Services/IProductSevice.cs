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
        Task<Response<ImageDTO>> AddImageItem(Guid id, string Thumbnails);
        Task<BaseResponse> ChangeStatus(Guid id);
        Task<BaseResponse> ChangeThumbnails(Guid id, string Thumbnails);
        Task<BaseResponse> Delete(Guid id);
        Task<BaseResponse> DeleteImageItem(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<PagingResponse<ListProductDTO>> Filter(FilterRequest filterRequest);
        Task<PagingResponse<ListProductDTO>> GetAll(PagingRequest pagingRequest, int? status = 1);
        Task<List<ListProductDTO>> GetAll(int? status = 1);
        Task<PagingResponse<ListProductDTO>> GetAllNoTracking(PagingRequest pagingRequest, int? status = 1);
        Task<PagingResponse<ListProductDTO>> GetByBrandId(PagingRequest pagingRequest);
        Task<PagingResponse<ListProductDTO>> GetByCategoryId(PagingRequest pagingRequest);
        Task<ProductDTO> GetById(Guid id, int? status = 2);
        Task<PagingResponse<ListProductDTO>> GetByKeyword(PagingRequest pagingRequest, string keyWork);
        Task<ProductCartItem> GetCartById(Guid productId, Guid optionId, Guid colorId);
        Task<BaseResponse> Update(ProductRequest productRequest);
    }
}
