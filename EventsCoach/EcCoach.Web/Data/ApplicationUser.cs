using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

    }
}
