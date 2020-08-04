using DSM.UI.Api.Models.DatabasePortal;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Helpers
{
    public class DSMDatabasePortalDbContext : DbContext
    {
        private static DSMDatabasePortalDbContext _instance;
        public static DSMDatabasePortalDbContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DSMDatabasePortalDbContext();
            }
            return _instance;
        }
        protected DSMDatabasePortalDbContext()
        {
        }

        public DSMDatabasePortalDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<EnvanterSQL> EnvanterSQL { get; set; }
        public DbSet<EnvanterPostgres> EnvanterPostgres { get; set; }
        public DbSet<EnvanterOracle> EnvanterOracle { get; set; }
    }
}
