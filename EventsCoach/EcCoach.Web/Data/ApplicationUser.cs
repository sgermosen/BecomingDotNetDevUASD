using EcCoach.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool IsAdmin { get; set; }

      //  public int OwnerID { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
            Attendances = new Collection<Attendance>();
        }

        internal void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));           
        }

    }
}
