using Entities.Models;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.Responses;
using Entities.Requests;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMailService _mailService;

        public OrderService(IMapper mapper,IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailService orderDetailService, IMailService mailService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderDetailService = orderDetailService;
            _mailService = mailService;
        }
        public async Task<BaseResponse> Add(PaymentRequest paymentRequest)
        {
            try
            {

                var order = _mapper.Map<Order>(paymentRequest);
                /*{
                    FirstName = paymentRequest.Name,
                    LastName = paymentRequest.Email,
                    PhoneNumber = paymentRequest.Phone,
                    Address = paymentRequest.Address,

                };*/

                await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                //
                foreach (var item in paymentRequest.Carts)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.Amount = item.quantity;
                    orderDetail.OrderId = order.Id;
                    orderDetail.productOptionId = item.ProductOption.Id;
                    orderDetail.ProductColorId = item.ProductOption.ProductColor.Id;
                    orderDetail.ProductId = item.ProductOption.Product.Id;

                    var ResultAddDetail = await _orderDetailService.Add(orderDetail);
                    if (!ResultAddDetail.IsSuccess)
                    {
                        return new BaseResponse(false, ResultAddDetail.Message);
                    }
                }
                //
                string subject = "Thanh toán thành công";
                string body = "thanh toán thành công";
                var send = await _mailService.SeedMail("vucao005@gmail.com", paymentRequest.Email, paymentRequest.Name, subject, body);
                if (!send.IsSuccess)
                {
                    return new BaseResponse(true, "Thanh toán thành công, chưa gửi được mail mail!" + send.Message);
                }

                //
                return new BaseResponse(true, "Thanh toán thành công, vui lòng check mail!");
            }
            catch (System.Exception ex)
            {

                return new BaseResponse(true, ex.Message);
            }
            
        }
    }

}
