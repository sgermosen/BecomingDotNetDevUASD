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
            .WithMany()
            .HasForeignKey(a => a.EventId) ;

            builder.Entity<Attendance>()
            .HasOne(a => a.Attendee)
            .WithMany()
            .HasForeignKey(a => a.AttendeeId) ; //.

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
