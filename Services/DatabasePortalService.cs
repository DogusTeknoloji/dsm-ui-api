using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.DatabasePortal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DSM.UI.Api.Services
{
    public interface IDatabasePortalService
    {
        Task<IEnumerable<DbInventory>> GetEnvanterAllDBEnvanter(int page);
        Task<DbInventory> Details(int id);
        IEnumerable<SearchResult> Search(object term);
        //public string GetServerCheckDate();
    }

    public class DatabasePortalService : IDatabasePortalService
    {
        private readonly DSMStorageDataContext _context;
        public DatabasePortalService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbInventory>> GetEnvanterAllDBEnvanter(int page)
        {
            return await _context.Dbinventory.AsNoTracking().Skip((page - 1) * 100).Take(100).ToListAsync();
        }

        public async Task<DbInventory> Details(int id)
        {
            return await _context.Dbinventory.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }



        public IEnumerable<SearchResult> Search(object term)
        {
            IEnumerable<PropertyInfo> stringProperties = typeof(DbInventory).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.Dbinventory select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results.Select(x => new SearchResult
            {
                ServerName = x.ServerName,
                Owner = x.Owner,
                Description = x.Description,
                Environment = x.Environment,
                IpAddress = x.IpAddress,
                Osversion = x.Osversion,
                Version = x.Version

            });
        }

        //  public string GetServerCheckDate()
        //  {
        //      var query = from a in _context.VCenterLogs where a.LogName == "ServerInventory" select a.LogValue;
        //      string checkDate = query.FirstOrDefault();
        //      checkDate = Convert.ToDateTime(checkDate).ToString(CultureInfo.InvariantCulture);
        //      return checkDate;
        //  }
    }
}
