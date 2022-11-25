using AutoMapper;
using CaoDinhVu.DAL.Data;
using Entities.DTOs;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public OrderDetailRepository(DBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IOrderDetailQuery BuildQuery()
        {
            return new OrderDetailQuery(_context.OrderDetails.AsQueryable(), _context);
        }

        public List<OrderInWeekResponse> OrderInWeek(DateTime dateTime)
        {
            var orderInWeek = _context.OrderDetails.Where(m => m.CreateAt.Value.Date >= dateTime.Date)
                                        .GroupBy(m => m.CreateAt.Value.Date)
                                        .Select(m=> 
                                                new OrderInWeekResponse()
                                                {
                                                        Day = m.Key.DayOfWeek.ToString(),
                                                        Amount = m.Sum(a=>a.Amount.Value)
                                                    }
                                        ).ToList();
            /*var result = new List<OrderInWeekResponse>();
            foreach (var item in orderInWeek)
            {
                var itemOrderInWeek = new OrderInWeekResponse();
                itemOrderInWeek.Amount = item.Amount;
                itemOrderInWeek.Day = GetDayOfWeek((int)item.Day.DayOfWeek);
                result.Add(itemOrderInWeek);
            }*/
            /*var listCongViec = new List<DashResponse>();
            a.ToList().ForEach(group =>
            {
                listCongViec.Add(new DashResponse
                {
                    Day = group.Key.DayOfWeek.ToString(),
                    Amount = group.Sum(m=>m.Amount.Value)
                    //SoLuong = group?.Count(item => item != null)
                });

            });*/

            return orderInWeek;
        }
        public async Task<long> SumAmountInDay(DateTime dateTime)
        {
            long amount = await BuildQuery().FilterByDay(dateTime).SumAsync(m => m.Amount.Value);
            return amount;
        }
        
        private string GetDayOfWeek(int day)
        {
            var dayOfWeek = new List<string>()
            {
                "Chủ nhật","Thứ hai","Thứ ba","Thứ tư","Thứ năm","Thứ sáu","Thứ 7"
            };
            return dayOfWeek[day];
        }
        public Task<OrderDetailDTO> GetAllOrderdetail()
        {
            try
            {
                var Result = from od in _context.OrderDetails
                             join p in _context.Products
                             on od.ProductId equals p.Id
                             join pc in _context.ProductColors
                             on od.ProductColorId equals pc.Id
                             join po in _context.ProductOptions
                             on od.productOptionId equals po.Id
                             select new
                             {
                                 OrderDetailDTO = _mapper.Map<OrderDetailDTO>(od)
                             };
                return null;
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
        public List<OrderByCategoryResponse> OrderByCategory()
        {
            try
            {
                var OrderByCategory = _context.OrderDetails 
                                                    .GroupBy(m => m.Product.CategoryId)
                                                    
                                                    .Select(m=> new OrderByCategoryResponse()
                                                      {
                                                          CategoryName = _context.Categories.Where(c=>c.Id == m.Key).Select(c=>c.Name).FirstOrDefault(),
                                                          Amount = m.Sum(a => a.Amount.Value)
                                                      }).ToList();
                                      
                                     /* select new OrderByCategoryResponse()
                                                {
                                                    CategoryName = category.Name,
                                                    Amount = aa.Sum(a => a.Amount.Value)
                                                };*/
                //var a = OrderByCategory;
                return OrderByCategory;
            }
            catch (Exception ex)
            {
                return null;
                throw new(ex.Message);
            }
        }
        public List<OrderByBrandResponse> OrderByBrand()
        {
            try
            {
                var OrderByBrand = _context.OrderDetails.GroupBy(m => m.Product.BrandId)
                                                        .Select(m => new OrderByBrandResponse()
                                                            {
                                                                BrandName = _context.Brands.Where(c => c.Id == m.Key)
                                                                                                  .Select(c => c.Name)
                                                                                                  .FirstOrDefault(),
                                                                Amount = m.Sum(a => a.Amount.Value)
                                                            }).ToList();

                return OrderByBrand;
            }
            catch (Exception ex)
            {
                return null;
                throw new(ex.Message);
            }
        }
        

    }
}
