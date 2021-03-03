using System;
using ElevenFiftySports.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.CustomerModels
{
    public class CustomerDetail
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }

        public string Email { get; set; }

        public string CellPhoneNumber { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name ="Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
