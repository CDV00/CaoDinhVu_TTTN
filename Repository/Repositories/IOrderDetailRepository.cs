using Entities.DTOs;
using Entities.Models;
using Query.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        IOrderDetailQuery BuildQuery();
        List<OrderByBrandResponse> OrderByBrand();
        List<OrderByCategoryResponse> OrderByCategory();
        List<OrderInWeekResponse> OrderInWeek(DateTime dateTime);
        Task<long> SumAmountInDay(DateTime dateTime);
    }
}
