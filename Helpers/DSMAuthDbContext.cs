using DSM.UI.Api.Models;
using DSM.UI.Api.Models.User;
using DSM.UI.Api.Models.WebAccessLogs;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Helpers
{
    public class DSMAuthDbContext : DbContext
    {
        public DSMAuthDbContext(DbContextOptions<DSMAuthDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<WebAccessLog> WebAccessLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey("Id");
                e.Property("Id").ValueGeneratedOnAdd();

                e.ToTable("Users");

                e.HasOne(d => d.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(d => d.RoleId);

                e.HasOne(d => d.Domain)
                .WithMany(x => x.Users)
                .HasForeignKey(d => d.DomainId);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles");
            });

            modelBuilder.Entity<Domain>(e =>
            {
                e.ToTable("Domains");
            });

            modelBuilder.Entity<WebAccessLog>(entity =>
            {
                entity.ToTable("WebAccessLogs");
                entity.Property(p => p.LogId).ValueGeneratedOnAdd();

                entity.HasKey(x => x.LogId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
