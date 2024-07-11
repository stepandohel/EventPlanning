using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<FieldDescription> FieldDescriptions { get; set; }
        public DbSet<EventField> EventFields { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Events);

            builder.Entity<Event>()
                .HasMany(x => x.Members)
                .WithMany(x => x.EventsMemberships)
                .UsingEntity<UserEvent>(
                x => x.HasOne<User>().WithMany(x => x.UserEvents).OnDelete(DeleteBehavior.Restrict),
                y => y.HasOne<Event>().WithMany(x => x.UserEvents).OnDelete(DeleteBehavior.Cascade));

            builder.Entity<EventField>()
                .HasOne(x => x.FieldDescription)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
