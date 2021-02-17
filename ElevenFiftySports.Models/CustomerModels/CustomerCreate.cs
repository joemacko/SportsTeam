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
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Please limit this field to 20 characters")]
        public string Name { get; set; }

        [MaxLength(40)]
        public string CustomerName { get; set; }
    }
}
