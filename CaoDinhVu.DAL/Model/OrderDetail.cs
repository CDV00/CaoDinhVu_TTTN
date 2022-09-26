﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Model
{
    public class OrderDetail : BaseEntiity<Guid>
    {
        public Guid OrderId { get; set; }
        public Product Product { get; set; }
        public int Status { get; set; }
    }
}