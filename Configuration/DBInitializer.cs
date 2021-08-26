using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Configuration
{
    public class DBInitializer
    {
        public enum DBContextType
        {
            DSMStorageServer,
            DSMAuthServer,
            DSMVCenterServer
        }

        public static void Initialize(string connectionString, DBContextType type)
        {
            switch (type)
            {
                case DBContextType.DSMStorageServer:
                    using (DSMStorageDataContext context = new DSMStorageDataContext(connectionString))
                    {
                        context.Database.Migrate();
                    }
                    break;
                case DBContextType.DSMAuthServer:
                    using (DSMAuthDbContext context = new DSMAuthDbContext(connectionString))
                    {
                        context.Database.Migrate();
                    }
                    break;
                case DBContextType.DSMVCenterServer:
                    using (DSMVCenterDbContext context = new DSMVCenterDbContext(connectionString))
                    {
                        context.Database.Migrate();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
