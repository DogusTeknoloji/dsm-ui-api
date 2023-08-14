using DSM.Core.Models;
using DSM.UI.Api.Models;
using DSM.UI.Api.Models.AzureDevOps;
using DSM.UI.Api.Models.Company;
using DSM.UI.Api.Models.CustomerUrlLists;
using DSM.UI.Api.Models.CustomInventories;
using DSM.UI.Api.Models.CustomModels;
using DSM.UI.Api.Models.Dashboard;
using DSM.UI.Api.Models.DatabasePortal;
using DSM.UI.Api.Models.EditableInventory;
using DSM.UI.Api.Models.LogModels;
using DSM.UI.Api.Models.Monitoring;
using DSM.UI.Api.Models.Reports;
using DSM.UI.Api.Models.Server;
using DSM.UI.Api.Models.UploadedFiles;
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
            modelBuilder.Entity<DbInventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DBInventory");

                entity.Property(e => e.Aginstalled).HasColumnName("AGInstalled");

                entity.Property(e => e.AktifPasif).HasMaxLength(255);

                entity.Property(e => e.Asm).HasColumnName("ASM");

                entity.Property(e => e.BackupAciklama).IsUnicode(false);

                entity.Property(e => e.BackupHistory).IsUnicode(false);

                entity.Property(e => e.BackupHistoryHata).IsUnicode(false);

                entity.Property(e => e.BackupMonitoring)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BackupMonitoringHata).IsUnicode(false);

                entity.Property(e => e.BackupPolicy).IsUnicode(false);

                entity.Property(e => e.BackupPolicyHata).IsUnicode(false);

                entity.Property(e => e.CollationType)
                    .HasColumnName("Collation_Type")
                    .IsUnicode(false);

                entity.Property(e => e.ConnectionScript)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cpuname)
                    .HasColumnName("CPUName")
                    .IsUnicode(false);

                entity.Property(e => e.Dbsearch)
                    .HasColumnName("DBSearch")
                    .IsUnicode(false);

                entity.Property(e => e.DbsearchHata)
                    .HasColumnName("DBSearchHata")
                    .IsUnicode(false);

                entity.Property(e => e.Dbtype).HasColumnName("DBType");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Domain).HasMaxLength(255);

                entity.Property(e => e.EditionInstalled).IsUnicode(false);

                entity.Property(e => e.Environment).HasMaxLength(255);

                entity.Property(e => e.FileGroupMonitoring)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileGroupMonitoringHata).IsUnicode(false);

                entity.Property(e => e.FileMonitoring)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileMonitoringHata).IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Instance)
                    .HasColumnName("instance")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasColumnName("IP_Address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsClustered).IsUnicode(false);

                entity.Property(e => e.JobMonitoring).IsUnicode(false);

                entity.Property(e => e.JobMonitoringHata).IsUnicode(false);

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.LogicalCpuCount).HasColumnName("Logical_CPU_Count");

                entity.Property(e => e.MachineType).IsUnicode(false);

                entity.Property(e => e.Osversion)
                    .HasColumnName("OSVersion")
                    .IsUnicode(false);

                entity.Property(e => e.PhysicalCpuCount).HasColumnName("Physical_CPU_Count");

                entity.Property(e => e.ProductBuildLevel).IsUnicode(false);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.ServerName).HasMaxLength(255);

                entity.Property(e => e.ServiceName).HasColumnName("Service_Name");

                entity.Property(e => e.Splevel)
                    .HasColumnName("SPLevel")
                    .IsUnicode(false);

                entity.Property(e => e.Sqlbit)
                    .HasColumnName("SQLBit")
                    .HasMaxLength(50);

                entity.Property(e => e.Version).IsUnicode(false);
            });

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

            modelBuilder.Entity<ApplicationServer>(entity => { entity.ToTable("ApplicationServerInventory"); });

            modelBuilder.Entity<ServerDisk>(entity =>
            {
                entity.ToTable("DiskStatus");
                entity.HasKey("DiskId");
                entity.HasOne(x => x.Server)
                    .WithMany(x => x.ServerDisks)
                    .HasForeignKey(x => x.ServerId);
            });

            modelBuilder.Entity<KPIMetricsView>(entity =>
            {
                entity.ToTable("KPIMetricsView");
                entity.HasNoKey();
            });

            modelBuilder.Entity<Alerts>(entity =>
            {
                entity.ToTable("Alerts");
                entity.HasKey("AlertId");
            });

            modelBuilder.Entity<AlertContacts>(entity =>
            {
                entity.ToTable("AlertContacts");
                entity.HasKey("AlertContactId");
            });

            modelBuilder.Entity<ExtendedContact>(entity =>
            {
                entity.ToTable("ExtendedContacts");
                entity.HasKey("ExtendedContactId");
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

            modelBuilder.Entity<ElasticSearchInventoryItem>(entity =>
            {
                entity.ToTable("ElasticSearchInventory");
                entity.HasKey("AppId");
            });

            modelBuilder.Entity<VCenterLog>(entity =>
            {
                entity.ToTable("VCenterLogs");
                entity.HasNoKey();
            });

            modelBuilder.Entity<AzurePortalInventory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("AzurePortalInventory");
            });

            modelBuilder.Entity<CustomerAppDbInventory>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("VdfAppDbInventory");
            });

            modelBuilder.Entity<CustomerExternalUrl>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("VdfExternalUrls");
            });

            modelBuilder.Entity<CustomerInternalUrl>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("VdfInternalUrls");
            });

            modelBuilder.Entity<OperationLog>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DSMOperationLogs");
            });

            modelBuilder.Entity<UpdatedSiteInventoryItem>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("UpdatedSiteInventory");
            });

            modelBuilder.Entity<DetailedServerInventoryItem>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("ReidinServerList");
            });

            modelBuilder.Entity<SentryListItem>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DTNobetCizelgesi");
            });

            modelBuilder.Entity<UploadedFileDetail>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("UploadedFiles");
            });

            modelBuilder.Entity<NetworkSecurityInventoryItem>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("NetworkSecurityNodes");
            });

            modelBuilder.Entity<NetworkInventoryItem>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("NetworkNodes");
            });
            
            modelBuilder.Entity<FrameworkVersionInventoryItem>(entity =>
            {
                entity.HasKey("id");
                entity.ToTable("FrameworkVersions");
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
        public DbSet<ElasticSearchInventoryItem> ElasticSearchInventory { get; set; }
        public DbSet<VCenterLog> VCenterLogs { get; set; }
        public DbSet<DbInventory> Dbinventory { get; set; }
        public DbSet<KPIMetricsView> KPIMetricsViews { get; set; }
        public DbSet<Alerts> Alert { get; set; }
        public DbSet<ExtendedContact> ExtendedContact { get; set; }
        public DbSet<AlertContacts> AlertContacts { get; set; }
        public DbSet<AzurePortalInventory> AzurePortalInventory { get; set; }
        public DbSet<CustomerAppDbInventory> CustomerAppDbInventories { get; set; }
        public DbSet<CustomerExternalUrl> CustomerExternalUrls { get; set; }
        public DbSet<CustomerInternalUrl> CustomerInternalUrls { get; set; }
        public DbSet<UpdatedSiteInventoryItem> UpdatedSiteInventoryItems { get; set; }
        public DbSet<DetailedServerInventoryItem> DetailedServerInventoryItems { get; set; }
        public DbSet<SentryListItem> SentryListItems { get; set; }
        public DbSet<UploadedFileDetail> UploadedFileDetails { get; set; }
        public DbSet<OperationLog> OperationLogs { get; set; }

        public DbSet<NetworkSecurityInventoryItem> NetworkSecurityInventoryItems { get; set; }
        public DbSet<NetworkInventoryItem> NetworkInventoryItems { get; set; }
        
        public DbSet<FrameworkVersionInventoryItem> FrameworkVersionInventoryItems { get; set; }
    }
}