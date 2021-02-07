using EcCoach.Web.Helpers;
using EcCoach.Web.Interfaces;
using EcCoach.Web.Models;
using EcCoach.Web.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcCoach.Web.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserFactory _currentUser;

        public DataContext(DbContextOptions<DataContext> options, ICurrentUserFactory currentUserFactory)
            : base(options)
        {
            _currentUser = currentUserFactory;
        }

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
            AddMyFilters(ref builder);

        }
     
        #region Properties to become tables on databases
        public DbSet<Event> Events { get; set; }

        public DbSet<Models.Type> Types { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        #endregion

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public override async Task< int> SaveChangesAsync(bool accepAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            MakeAudit();
            return await base.SaveChangesAsync(accepAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            MakeAudit();
            return await base.SaveChangesAsync( cancellationToken);
        }

        private void MakeAudit()
        {
            var modifiendEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity && (
                x.State == EntityState.Added || x.State == EntityState.Modified));

            var transactionUser = new CurrentUser();

            if (_currentUser != null)
                transactionUser = _currentUser.Get;

            foreach (var entry in modifiendEntries)
            {
                if(entry.Entity is AuditEntity entity)
                {
                    var date = DateTime.Now;
                    string userId = transactionUser.UserId;
                     
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;

                }

            }

        }

        private void AddMyFilters ( ref ModelBuilder modelBuilder)
        {
            var user = new CurrentUser();

            if (_currentUser != null)
                user = _currentUser.Get;

            modelBuilder.Entity<Event>().HasQueryFilter(x => !x.Deleted);
            
        }
    }
}
