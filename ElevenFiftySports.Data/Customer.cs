using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Customer
    {
        
        [Key]
        public Guid CustomerId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CellPhoneNumber { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
