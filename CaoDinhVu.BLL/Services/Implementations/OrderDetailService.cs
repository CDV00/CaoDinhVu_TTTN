using Entities.Models;
using System;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.Responses;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IMapper mapper,IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse> Add(OrderDetail orderDetail)
        {

            try
            {
                await _orderDetailRepository.CreateAsync(orderDetail);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);
            }
            catch (Exception)
            {

                return new BaseResponse(true, "có lỗi khi thêm chi tiêt đon hàng");
            }
        }
    }

}
