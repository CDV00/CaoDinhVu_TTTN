﻿using Entities.Models;
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
    }
}
