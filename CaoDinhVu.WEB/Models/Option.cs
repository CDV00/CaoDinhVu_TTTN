﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Models
{
    public class Option:BaseEntity<Guid>
    {
        public int RAM { get; set; }
        public int ROM { get; set; }
    }
}