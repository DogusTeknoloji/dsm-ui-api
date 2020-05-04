using DSM.Core.Models;
using DSM.UI.Api.Models;
using DSM.UI.Api.Models.Company;
using DSM.UI.Api.Models.Dashboard;
using DSM.UI.Api.Models.Reports;
using DSM.UI.Api.Models.Server;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api
{
    public class DSMStorageDataContext : DbContext
    {
        private static DSMStorageDataContext _instance;
        public static DSMStorageDataContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DSMStorageDataContext();
            }
            return _instance;
        }
        protected DSMStorageDataContext()
        {
        }

        public DSMStorageDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("IISSite");

                entity.Property(e => e.AppType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IISSiteId).HasColumnName("IISSiteId");

                entity.Property(e => e.LastCheckTime).HasColumnType("datetime");

                entity.Property(e => e.NetFrameworkVersion).HasMaxLength(50);

                entity.Property(e => e.WebConfigBackupDirectory)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WebConfigLastBackupDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SiteBinding>(entity =>
            {
                entity.ToTable("IISSiteBinding");

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_SiteId");
                entity.Property(e => e.IpAddressFamily).HasColumnType("IpAdressFamily");
                entity.Property(e => e.IsSSLBound).HasColumnName("IsSSLBound");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Bindings)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_dbo.IISSiteBinding_dbo.IISSite_SiteId");
            });

            modelBuilder.Entity<SiteConnectionString>(entity =>
            {
                entity.ToTable("IISSiteConnectionString");

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_SiteId");

                entity.Property(e => e.ConnectionName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.ConnectionStrings)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_dbo.IISSiteConnectionString_dbo.IISSite_SiteId");
            });

            modelBuilder.Entity<SiteEndpoint>(entity =>
            {
                entity.ToTable("IISSiteEndpoint");

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_SiteId");

                entity.Property(e => e.EndpointName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Endpoints)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_dbo.IISSiteEndpoint_dbo.IISSite_SiteId");
            });

            modelBuilder.Entity<SitePackage>(entity =>
            {

                entity.ToTable("IISSitePackageVersion");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Version).HasMaxLength(100);

                entity.HasOne(e => e.Site)
                      .WithMany(d => d.Packages)
                      .HasForeignKey(e => e.SiteId)
                      .HasConstraintName("FK_IISSitePackageVersion_IISSite");
            });

            modelBuilder.Entity<SiteWebConfiguration>(entity =>
            {
                entity.HasKey(x => x.SiteId);

                entity.ToTable("IISSiteWebConfiguration");

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_Id");

                entity.Property(e => e.SiteId).ValueGeneratedNever();

                entity.Property(e => e.ContentRaw).HasColumnType("text");

                entity.HasOne(d => d.Site)
                    .WithOne(p => p.Configuration)
                    .HasForeignKey<SiteWebConfiguration>(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IISSiteWebConfiguration_dbo.IISSite_Id");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("ServerInventoryX");

                entity.HasOne(x => x.Company)
                .WithMany(x => x.Servers)
                .HasForeignKey(x => x.CompanyId)
                .HasConstraintName("FK_ServerInventory_ServerInventory");

            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.CompanyId);
                entity.ToTable("Companies");
            });

            modelBuilder.Entity<ApplicationServer>(entity =>
            {
                entity.ToTable("ApplicationServerInventory");
            });

            modelBuilder.Entity<ServerDisk>(entity =>
            {
                entity.ToTable("DiskStatus");
                entity.HasKey("DiskId");
                entity.HasOne(x => x.Server)
                .WithMany(x => x.ServerDisks)
                .HasForeignKey(x => x.ServerId);
            });

            modelBuilder.Entity<ScheduledJobItem>(entity =>
            {
                entity.ToTable("JobInventory");
                entity.HasKey("ObjectName");
            });

            modelBuilder.Entity<AppManagementLink>(entity =>
            {
                entity.ToTable("AppManagementLinks");
                entity.HasKey("AppId");
            });

            modelBuilder.Entity<VCenterLog>(entity =>
            {
                entity.ToTable("VCenterLogs");
                entity.HasNoKey();
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteBinding> Bindings { get; set; }
        public DbSet<SiteConnectionString> ConnectionStrings { get; set; }
        public DbSet<SiteEndpoint> Endpoints { get; set; }
        public DbSet<SitePackage> Packages { get; set; }
        public DbSet<SiteWebConfiguration> WebConfigurations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationServer> ApplicationServers { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerDisk> ServerDisks { get; set; }
        public DbSet<ScheduledJobItem> ScheduledJobItems { get; set; }
        public DbSet<AppManagementLink> AppManagementLinks { get; set; }
        public DbSet<VCenterLog> VCenterLogs { get; set; }
    }
}
