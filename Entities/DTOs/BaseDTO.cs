﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BaseDTO
    {
        public Guid? Id { get; set; } = Guid.Empty;
    }
}
