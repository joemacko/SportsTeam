using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.CustomerModels
{
    public class CustomerCreate
    {
        [Required]
        public string Name { get; set; }

        public string CustomerName { get; set; }
    }
}
