using CaoDinhVu.DAL.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class OrderQuery : QueryBase<Order>, IOrderQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public OrderQuery(IQueryable<Order> orderQuery, DBContext dbContext) : base(orderQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IOrderQuery FiterById(Guid id)
        {
            Query = Query.Where(or => or.Id == id);
            return this;
        }
        public IOrderQuery FiterByUserId(Guid id)
        {
            Query = Query.Where(or => or.CreateBy == id);
            return this;
        }
        public IOrderQuery FiterStatus(int status)
        {
            if(status == 2)
            {
                Query = Query.Where(or => or.StatusOrder != 0);
            }
            else if(status == 0)
            {
                Query = Query.Where(or => or.StatusOrder == 0);
            }
            return this;
        }
        public IOrderQuery FiterStatusS(int status)
        {
            if(status == 4)
            {
                return this;
            }
            Query = Query.Where(or => or.StatusOrder == status);
            return this;
        }
        public IOrderQuery IncludeDetail()
        {
            Query.Include(or => or.OrderDetails).Load();
            Query.Include(or => or.OrderDetails).ThenInclude(m=>m.Product).Load();
            Query.Include(or => or.OrderDetails).ThenInclude(m=>m.ProductColor).Load();
            Query.Include(or => or.OrderDetails).ThenInclude(m=>m.productOption).Load();
            return this;
        }
    }
}
