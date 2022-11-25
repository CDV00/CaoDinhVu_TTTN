using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface IOrderQuery : IQuery<Order>
    {
        IOrderQuery FiterById(Guid id);
        IOrderQuery FiterByUserId(Guid id);
        IOrderQuery FiterStatus(int status);
        IOrderQuery IncludeDetail();
    }
}
