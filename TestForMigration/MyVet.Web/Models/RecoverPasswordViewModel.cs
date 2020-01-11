using System.ComponentModel.DataAnnotations;

namespace MyVet.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
