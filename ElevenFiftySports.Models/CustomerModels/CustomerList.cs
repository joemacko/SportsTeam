using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.CustomerModels
{
    class CustomerList
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
