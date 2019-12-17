using DSM.UI.Api.Models;
using DSM.UI.Api.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Domain> Domains { get; set; }
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
