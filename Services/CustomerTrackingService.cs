using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.CustomerUrlLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DSM.UI.Api.Services
{
    public interface ICustomerTrackingService
    {
        Task<IEnumerable<CustomerAppDbInventory>> GetAllCustomerAppDbInventoriesAsync();
        Task<IEnumerable<CustomerExternalUrl>> GetAllCustomerExternalUrlsAsync();
        Task<IEnumerable<CustomerInternalUrl>> GetAllCustomerInternalUrlsAsync();

        Task<CustomerAppDbInventory> GetCustomerAppDbInventoryAsync(int id);
        Task<CustomerExternalUrl> GetCustomerExternalUrlAsync(int id);
        Task<CustomerInternalUrl> GetCustomerInternalUrlAsync(int id);

        Task<CustomerAppDbInventory> AddCustomerAppDbInventoryAsync(CustomerAppDbInventory customerAppDbInventory);
        Task<CustomerExternalUrl> AddCustomerExternalUrlAsync(CustomerExternalUrl customerExternalUrl);
        Task<CustomerInternalUrl> AddCustomerInternalUrlAsync(CustomerInternalUrl customerInternalUrl);

        Task<CustomerAppDbInventory> UpdateCustomerAppDbInventoryAsync(CustomerAppDbInventory customerAppDbInventory);
        Task<CustomerExternalUrl> UpdateCustomerExternalUrlAsync(CustomerExternalUrl customerExternalUrl);
        Task<CustomerInternalUrl> UpdateCustomerInternalUrlAsync(CustomerInternalUrl customerInternalUrl);

        Task<bool> DeleteCustomerAppDbInventoryAsync(int id);
        Task<bool> DeleteCustomerExternalUrlAsync(int id);
        Task<bool> DeleteCustomerInternalUrlAsync(int id);

        byte[] DownloadAppDbInventory();
        byte[] DownloadAppDbInventory(object term);
        byte[] DownloadExternalUrls();
        byte[] DownloadExternalUrls(object term);
        byte[] DownloadInternalUrls();
        byte[] DownloadInternalUrls(object term);
    }

    public class CustomerTrackingService : ICustomerTrackingService
    {
        private readonly DSMStorageDataContext _context;

        public CustomerTrackingService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerAppDbInventory>> GetAllCustomerAppDbInventoriesAsync()
        {
            // Query v2 
            return await _context.CustomerAppDbInventories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CustomerExternalUrl>> GetAllCustomerExternalUrlsAsync()
        {
            // Query v2
            return await _context.CustomerExternalUrls.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CustomerInternalUrl>> GetAllCustomerInternalUrlsAsync()
        {
            // Query v2
            return await _context.CustomerInternalUrls.AsNoTracking().ToListAsync();
        }

        public async Task<CustomerAppDbInventory> GetCustomerAppDbInventoryAsync(int id)
        {
            return await _context.CustomerAppDbInventories.FindAsync(id);
        }

        public async Task<CustomerExternalUrl> GetCustomerExternalUrlAsync(int id)
        {
            return await _context.CustomerExternalUrls.FindAsync(id);
        }

        public async Task<CustomerInternalUrl> GetCustomerInternalUrlAsync(int id)
        {
            return await _context.CustomerInternalUrls.FindAsync(id);
        }

        public async Task<CustomerAppDbInventory> AddCustomerAppDbInventoryAsync(
            CustomerAppDbInventory customerAppDbInventory)
        {
            _context.CustomerAppDbInventories.Add(customerAppDbInventory);
            await _context.SaveChangesAsync();
            return customerAppDbInventory;
        }

        public async Task<CustomerExternalUrl> AddCustomerExternalUrlAsync(CustomerExternalUrl customerExternalUrl)
        {
            _context.CustomerExternalUrls.Add(customerExternalUrl);
            await _context.SaveChangesAsync();
            return customerExternalUrl;
        }

        public async Task<CustomerInternalUrl> AddCustomerInternalUrlAsync(CustomerInternalUrl customerInternalUrl)
        {
            _context.CustomerInternalUrls.Add(customerInternalUrl);
            await _context.SaveChangesAsync();
            return customerInternalUrl;
        }

        public async Task<CustomerAppDbInventory> UpdateCustomerAppDbInventoryAsync(
            CustomerAppDbInventory customerAppDbInventory)
        {
            if (!_context.CustomerAppDbInventories.Any(x => x.Id == customerAppDbInventory.Id))
                return null;

            _context.CustomerAppDbInventories.Update(customerAppDbInventory);
            await _context.SaveChangesAsync();
            return customerAppDbInventory;
        }

        public async Task<CustomerExternalUrl> UpdateCustomerExternalUrlAsync(CustomerExternalUrl customerExternalUrl)
        {
            if (!_context.CustomerExternalUrls.Any(x => x.Id == customerExternalUrl.Id))
                return null;

            _context.CustomerExternalUrls.Update(customerExternalUrl);
            await _context.SaveChangesAsync();
            return customerExternalUrl;
        }

        public async Task<CustomerInternalUrl> UpdateCustomerInternalUrlAsync(CustomerInternalUrl customerInternalUrl)
        {
            if (!_context.CustomerInternalUrls.Any(x => x.Id == customerInternalUrl.Id))
                return null;

            _context.CustomerInternalUrls.Update(customerInternalUrl);
            await _context.SaveChangesAsync();
            return customerInternalUrl;
        }

        public async Task<bool> DeleteCustomerAppDbInventoryAsync(int id)
        {
            var customerAppDbInventory = await _context.CustomerAppDbInventories.FindAsync(id);

            if (customerAppDbInventory == null)
                return false;

            _context.CustomerAppDbInventories.Remove(customerAppDbInventory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerExternalUrlAsync(int id)
        {
            var customerExternalUrl = await _context.CustomerExternalUrls.FindAsync(id);

            if (customerExternalUrl == null)
                return false;

            _context.CustomerExternalUrls.Remove(customerExternalUrl);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerInternalUrlAsync(int id)
        {
            var customerInternalUrl = await _context.CustomerInternalUrls.FindAsync(id);

            if (customerInternalUrl == null)
                return false;

            _context.CustomerInternalUrls.Remove(customerInternalUrl);
            await _context.SaveChangesAsync();
            return true;
        }

        public byte[] DownloadAppDbInventory()
        {
            return DownloadAppDbInventory(null);
        }

        public byte[] DownloadAppDbInventory(object term)
        {
            IEnumerable<CustomerAppDbInventory> results = null;
            if (term == null)
            {
                var query = _context.CustomerAppDbInventories.AsQueryable();
                results = query.ToList().Select(
                    x => new CustomerAppDbInventory
                    {
                        Id = x.Id,
                        AppName = x.AppName,
                        DbName = x.DbName,
                        Server = x.Server,
                        DBIP = x.DBIP,
                        DBPort = x.DBPort
                    }
                );
            }
            else
            {
                var query = _context.CustomerAppDbInventories.AsQueryable();
                var stringProperties = typeof(CustomerAppDbInventory).GetProperties()
                    .Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, stringProperties, term.ToString());

                results = query.ToList().Select(
                    x => new CustomerAppDbInventory
                    {
                        Id = x.Id,
                        AppName = x.AppName,
                        DbName = x.DbName,
                        Server = x.Server,
                        DBIP = x.DBIP,
                        DBPort = x.DBPort
                    }
                );
            }

            return ExcelOperations.ExportToExcel(results);
        }

        public byte[] DownloadExternalUrls()
        {
            return DownloadExternalUrls(null);
        }

        public byte[] DownloadExternalUrls(object term)
        {
            IEnumerable<CustomerExternalUrl> results = null;
            if (term == null)
            {
                var query = _context.CustomerExternalUrls.AsQueryable();
                results = query.ToList().Select(
                    x => new CustomerExternalUrl
                    {
                        Id = x.Id,
                        ApplicationTeam = x.ApplicationTeam,
                        FrontApp = x.FrontApp,
                        SourceIpPort = x.SourceIpPort,
                        DestinationURL = x.DestinationURL,
                        DestinationIP_Port = x.DestinationIP_Port,
                        IsFollowing = x.IsFollowing,
                        ServiceExplanation = x.ServiceExplanation,
                        FromServer = x.FromServer,
                        LoadBalancerIP = x.LoadBalancerIP
                    }
                );
            }
            else
            {
                var query = _context.CustomerExternalUrls.AsQueryable();
                var stringProperties = typeof(CustomerExternalUrl).GetProperties()
                    .Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, stringProperties, term.ToString());

                results = query.ToList().Select(
                    x => new CustomerExternalUrl
                    {
                        Id = x.Id,
                        ApplicationTeam = x.ApplicationTeam,
                        FrontApp = x.FrontApp,
                        SourceIpPort = x.SourceIpPort,
                        DestinationURL = x.DestinationURL,
                        DestinationIP_Port = x.DestinationIP_Port,
                        IsFollowing = x.IsFollowing,
                        ServiceExplanation = x.ServiceExplanation,
                        FromServer = x.FromServer,
                        LoadBalancerIP = x.LoadBalancerIP
                    }
                );
            }

            return ExcelOperations.ExportToExcel(results);
        }
        
        public byte[] DownloadInternalUrls()
        {
            return DownloadInternalUrls(null);
        }

        public byte[] DownloadInternalUrls(object term)
        {
            IEnumerable<CustomerInternalUrl> results = null;
            if (term == null)
            {
                var query = _context.CustomerInternalUrls.AsQueryable();
                results = query.ToList().Select(
                    x => new CustomerInternalUrl
                    {
                        Id = x.Id,
                        ApplicationTeam = x.ApplicationTeam,
                        FrontApp = x.FrontApp,
                        SourceIpPort = x.SourceIpPort,
                        DestinationURL = x.DestinationURL,
                        DestinationIP_Port = x.DestinationIP_Port,
                        IsFollowing = x.IsFollowing,
                        ServiceExplanation = x.ServiceExplanation,
                        FromServer = x.FromServer,
                        LoadBalancerIP = x.LoadBalancerIP
                    }
                );
            }
            else
            {
                var query = _context.CustomerInternalUrls.AsQueryable();
                var stringProperties = typeof(CustomerInternalUrl).GetProperties()
                    .Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, stringProperties, term.ToString());

                results = query.ToList().Select(
                    x => new CustomerInternalUrl
                    {
                        Id = x.Id,
                        ApplicationTeam = x.ApplicationTeam,
                        FrontApp = x.FrontApp,
                        SourceIpPort = x.SourceIpPort,
                        DestinationURL = x.DestinationURL,
                        DestinationIP_Port = x.DestinationIP_Port,
                        IsFollowing = x.IsFollowing,
                        ServiceExplanation = x.ServiceExplanation,
                        FromServer = x.FromServer,
                        LoadBalancerIP = x.LoadBalancerIP
                    }
                );
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}