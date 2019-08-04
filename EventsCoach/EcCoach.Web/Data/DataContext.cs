using EcCoach.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcCoach.Web.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        #region Properties to become tables on databases
        public DbSet<Event> Events { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // old and simple and beautifull way of do this on ef6
            //builder.Entity<Attendance>.HasRequired(a => a.Event).WithMany(g => g.Attendances);

            //Defining the composed primary Key
            builder.Entity<Attendance>().HasKey(p => new { p.AttendeeId, p.EventId });

            //Relation
            builder.Entity<Attendance>()
            .HasOne(a => a.Event)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EventId);

            builder.Entity<Attendance>()
            .HasOne(a => a.Attendee)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.AttendeeId); //.


            builder.Entity<Following>().HasKey(p => new { p.FolloweeId, p.FollowerId });

            builder.Entity<Following>()
                    .HasOne(a => a.Follower)
                    .WithMany(c => c.Followers)
                    .HasForeignKey(a => a.FollowerId);

            builder.Entity<Following>()
                    .HasOne(a => a.Followee)
                    .WithMany(c => c.Followees)
                    .HasForeignKey(a => a.FolloweeId);

            builder.Entity<UserNotification>().HasKey(p => new { p.UserId, p.NotificationId });

            builder.Entity<UserNotification>()
                  .HasOne(a => a.User)
                  .WithMany(u => u.UserNotifications)
                  .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserNotification>()
                 .HasOne(a => a.Notification)
                 .WithMany(u => u.UserNotifications)
                 .HasForeignKey(a => a.NotificationId).OnDelete(DeleteBehavior.Restrict);

            var cascadeFKs = builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior ==
            DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(builder);

        }
    }
}
