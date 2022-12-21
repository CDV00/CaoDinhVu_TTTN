using Entities.Models;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.Responses;
using Entities.Requests;
using System;
using System.Collections.Generic;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMailService _mailService;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IConfiguration _config;

        public OrderService(IMapper mapper,IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailService orderDetailService, IMailService mailService, IOrderDetailRepository orderDetailRepository, IConfiguration config)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderDetailService = orderDetailService;
            _mailService = mailService;
            _orderDetailRepository = orderDetailRepository;
            _config = config;
        }
        public async Task<BaseResponse> Add(PaymentRequest paymentRequest)
        {
            try
            {

                var order = _mapper.Map<Order>(paymentRequest);
                

                await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                //
                //_config["Server:Location"]
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
                string link = _config["Server:Location"] + "don-hang?ma_don_hang=" + order.Id;
                string body = "<h2>thanh toán thành công</h2><p>Để theo dõi đơn hàng vui lòng <a href='"+ link + "'>Click</a></p>";
                var send = await _mailService.SeedMail("vucao005@gmail.com", paymentRequest.LastName, paymentRequest.FirstName, subject, body);
                if (!send.IsSuccess)
                {
                    return new BaseResponse(true, "Thanh toán thành công, chưa gửi được mail mail!" + send.Message);
                }

                //
                return new BaseResponse(true, "Thanh toán thành công, vui lòng check mail!");
            }
            catch (System.Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }
            
        }

        public async Task<Responses<OrderDTO>> GetAll(int? status = 1)
        {
            try
            {
                var orders =await _orderRepository.BuildQuery().FiterStatus(status.Value).ToListNoTrackingAsync(o => _mapper.Map<OrderDTO>(o));
                return new Responses<OrderDTO>(true, "Thành công", orders);
            }
            catch (Exception ex)
            {
                return new Responses<OrderDTO>(false, "Thất bại " + ex.Message);
            }
        }
        public async Task<Response<OrderDTO>> GetDetailById(Guid id)
        {
            try
            {
                var orders = await _orderRepository.BuildQuery().FiterById(id).IncludeDetail().AsSelectorAsync(o => _mapper.Map<OrderDTO>(o));
                return new Response<OrderDTO>(true, "Thành công", orders);
            }
            catch (Exception ex)
            {
                return new Response<OrderDTO>(false, "Thất bại " + ex.Message);
            }
        }
        public async Task<Responses<OrderDTO>> GetByUserId(Guid userId)
        {
            try
            {
                var orders = await _orderRepository.BuildQuery().FiterByUserId(userId).IncludeDetail().ToListAsync(o => _mapper.Map<OrderDTO>(o));
                return new Responses<OrderDTO>(true, "Thành công", orders);
            }
            catch (Exception ex)
            {
                return new Responses<OrderDTO>(false, "Thất bại " + ex.Message);
            }
        }


        public Responses<OrderInWeekResponse> GetOrderInWeek()
        {
            try
            {
                //ngày này tuần trước
                var dayLastWeek = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0)).AddDays(-6);
                var lineChart = _orderDetailRepository.OrderInWeek(dayLastWeek);
                var result = new List<OrderInWeekResponse>();
                int j = 0;
                for (int i =0; i < 7; i++)
                {
                    if(lineChart.Count == 0)
                    {
                        for(int day = 0; day < 7; day++)
                        {
                            var item = new OrderInWeekResponse();
                            item.Day = dayLastWeek.AddDays(day).DayOfWeek.ToString();
                            item.Amount = 0;
                            result.Add(item);
                        }
                        break;
                    }
                    else if(lineChart.Count > j && dayLastWeek.AddDays(i).DayOfWeek.ToString().Equals(lineChart[j].Day))
                    {
                        result.Add(lineChart[j]);
                        j++;
                    }
                    else
                    {
                        var item = new OrderInWeekResponse();
                        item.Day = dayLastWeek.AddDays(i).DayOfWeek.ToString();
                        item.Amount = 0;
                        result.Add(item);
                    }
                }
                return new Responses<OrderInWeekResponse>(true, "thanh cong", result);
                #region comment
                /*var listAmount = new List<long>();
                var listDay = new List<string>();
                for (int i= 0; i<7; i++)
                {
                    listAmount.Add(await _orderDetailService.GetAmountInDay(dayStart.AddDays(i)));
                    listDay.Add(GetDayOfWeek((int)dayStart.AddDays(i).DayOfWeek));
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                return new Responses<OrderInWeekResponse>(false, "that bai "+ex.Message);
            } 
        }
        public Responses<OrderByCategoryResponse> GetOrderByCategory()
        {
            try
            {
                var orderCategory = _orderDetailRepository.OrderByCategory();
                if(orderCategory.Count > 3)
                {
                    orderCategory[3].CategoryName = "khác";
                    for (int i = 4; i < orderCategory.Count; i++)
                    {
                        orderCategory[3].Amount += orderCategory[i].Amount;
                    }
                }
                return new Responses<OrderByCategoryResponse>(true, "Thành công ", orderCategory);
            }
            catch (Exception ex)
            {

                return new Responses<OrderByCategoryResponse>(false, "Thất bại " + ex.Message);
            }
        }
        public Responses<OrderByBrandResponse> GetOrderByBrand()
        {
            try
            {
                var orderBrand = _orderDetailRepository.OrderByBrand();
                if (orderBrand.Count > 3)
                {
                    orderBrand[3].BrandName = "khác";
                    for (int i = 4; i < orderBrand.Count; i++)
                    {
                        orderBrand[3].Amount += orderBrand[i].Amount;
                    }
                }
                return new Responses<OrderByBrandResponse>(true, "Thanh công " , orderBrand);
            }
            catch (Exception ex)
            {

                return new Responses<OrderByBrandResponse>(false, "thất bại " + ex.Message);
            }
        }
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order.StatusOrder == 1)
                    order.StatusOrder = 2;
                else
                    order.StatusOrder = 3;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, order.StatusOrder.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi trạng thái thất bại" + ex);
            }
        }
        public async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                order.StatusOrder = 0;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "xóa mềm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa mềm thất bại" + ex);
            }
        }
        public async Task<int> CountOrderByStatus(Guid userId, int? status = 4)
        {
            int countOrder = 0;
            try
            {
                countOrder = await _orderRepository.BuildQuery().FiterByUserId(userId).FiterStatusS(status.Value).CountAsync();
                return countOrder;
            }
            catch (Exception ex)
            {
                return countOrder;
                throw new Exception(ex.Message);
            }
        }
    }

}
