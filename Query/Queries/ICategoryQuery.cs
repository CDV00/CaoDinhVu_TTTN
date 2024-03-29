﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries
{
    public interface ICategoryQuery : IQuery<Category>
    {
        ICategoryQuery FilterStatus(int status);
        ICategoryQuery FiterById(Guid id);
        ICategoryQuery GetAll();
    }
}
