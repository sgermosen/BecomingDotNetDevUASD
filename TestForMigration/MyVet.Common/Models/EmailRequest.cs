using System.ComponentModel.DataAnnotations;

namespace MyVet.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
