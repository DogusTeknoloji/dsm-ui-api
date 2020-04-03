using DSM.UI.Api.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public interface IDashboardService
    {
        IEnumerable<AppManagementLink> GetLinks();
    }

    public class DashboardService : IDashboardService
    {
        private DSMStorageDataContext _context;
        public DashboardService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<AppManagementLink> GetLinks()
        {
            var query = this._context.AppManagementLinks.Where(x => x.IsActive);
            return query;
        }
    }
}
