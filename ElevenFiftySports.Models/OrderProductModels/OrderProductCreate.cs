﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderProductModels
{
    public class OrderProductCreate
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }

    }
}