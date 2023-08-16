using KnightsAndDragons.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KnightsAndDragons.Infrastructure.Data
{
    public class KnightsAndDragonsDbContext : DbContext
    {
        public DbSet<Knight> Knights { get; set; } = null!;
        public DbSet<Dragon> Dragons { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        public KnightsAndDragonsDbContext()
        {

        }

        public KnightsAndDragonsDbContext(DbContextOptions<KnightsAndDragonsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasMany(u => u.Dragons)
                 .WithOne(d => d.User)
                 .HasForeignKey(d => d.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasMany(u => u.Knights)
                 .WithOne(k => k.User)
                 .HasForeignKey(k => k.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
