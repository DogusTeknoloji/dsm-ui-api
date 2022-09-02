using DSM.UI.Api.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using DSM.UI.Api.Helpers;

namespace DSM.UI.Api.Services
{
    public interface IDashboardService
    {
        IEnumerable<AppManagementLink> GetLinks();
        IEnumerable<ElasticSearchInventoryDetails> GetElasticSearchInventory();
        string GetDashboard();
        CountOfServices GetAllCounts();
        int GetTotalSiteCount();
        int GetTotalServerCount();
        int GetTotalResponsibilityCount();
        int GetTotalCompanyCount();
        int GetTotalDbCount();
        int GetTotalUserCount();
    }

    public class DashboardService : IDashboardService
    {
        private readonly DSMStorageDataContext _context;
        private readonly DSMAuthDbContext _authContext;

        public DashboardService(DSMStorageDataContext context, DSMAuthDbContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        public string GetDashboard()
        {
            // Blank method for test. It won't be use
            return "Ok";
        }

        public CountOfServices GetAllCounts()
        {
            return new CountOfServices
            {
                SiteCount = GetTotalSiteCount(),
                ServerCount = GetTotalServerCount(),
                ResponsibleCount = GetTotalResponsibilityCount(),
                CompanyCount = GetTotalCompanyCount(),
                DatabasePortalCount = GetTotalDbCount(),
                TotalUserCount = GetTotalUserCount()
            };
        }

        public int GetTotalSiteCount()
        {
            return _context.Sites.Count();
        }

        public int GetTotalServerCount()
        {
            return _context.Servers.Count();
        }

        public int GetTotalResponsibilityCount()
        {
            return _context.Servers.Select(s => s.Responsible).Distinct().Count();
        }

        public int GetTotalCompanyCount()
        {
            return _context.Companies.Count();
        }

        public int GetTotalDbCount()
        {
            return _context.Dbinventory.Count();
        }

        public int GetTotalUserCount()
        {
            return _authContext.Users.Count();
        }

        public IEnumerable<AppManagementLink> GetLinks()
        {
            var query = this._context.AppManagementLinks.Where(x => x.IsActive);
            return query;
        }

        public IEnumerable<ElasticSearchInventoryDetails> GetElasticSearchInventory()
        {
            var query = this._context.ElasticSearchInventory.Where(x => x.IsActive);
            if (query.Count() < 1)
                return null;

            var results = query.Select(x => new ElasticSearchInventoryDetails
            {
                CompanyName = x.Company.Name,
                Description = x.Description,
                Hostname = x.Hostname,
                IpAddress = x.IpAddress,
                LoadbalancerIp = x.LoadbalancerIp,
                Password = x.Password,
                Url = x.Url,
                Username = x.Username,
                ServerId = x.ServerId,
                CompanyId = x.CompanyId
            });

            return results;
        }
    }
}