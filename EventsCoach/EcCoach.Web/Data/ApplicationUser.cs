using Microsoft.AspNetCore.Identity;

namespace EcCoach.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; }

        public string Address { get; set; }

    }
}
