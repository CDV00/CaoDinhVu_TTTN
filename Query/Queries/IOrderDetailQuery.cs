﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface IOrderDetailQuery : IQuery<OrderDetail>
    {
        IOrderDetailQuery FilterByDay(DateTime dateTime);
    }
}
