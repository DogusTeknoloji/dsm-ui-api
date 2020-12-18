using DSM.UI.Api.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public interface IDashboardService
    {
        IEnumerable<AppManagementLink> GetLinks();
        IEnumerable<ElasticSearchInventoryDetails> GetElasticSearchInventory();
        string GetDashboard();
    }

    public class DashboardService : IDashboardService
    {
        private readonly DSMStorageDataContext _context;
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
