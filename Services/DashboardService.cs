using DSM.UI.Api.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public interface IDashboardService
    {
        IEnumerable<AppManagementLink> GetLinks();
        string GetDashboard();
    }

    public class DashboardService : IDashboardService
    {
        private DSMStorageDataContext _context;
        public DashboardService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public string GetDashboard()
        {
            // Blank method for test. It won't be use
            return "Ok";
        }

        public IEnumerable<AppManagementLink> GetLinks()
        {
            var query = this._context.AppManagementLinks.Where(x => x.IsActive);
            return query;
        }
    }
}
