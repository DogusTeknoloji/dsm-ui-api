using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CustomHelpers;
using DSM.UI.Api.Models.CustomInventories;
using DSM.UI.Api.Models.CustomModels;
using DSM.UI.Api.Models.Server;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Services
{
    public interface ICustomInventoryService
    {
        Task<IList<NetworkInventoryItem>> GetNetworkInventory();
        Task<IList<NetworkSecurityInventoryItem>> GetNetworkSecurityInventory();
        Task<IList<FrameworkVersionInventoryItem>> GetFrameworkVersionInventory();
        Task<IList<EMBindingInventoryItem>> GetEmBindingInventory(int page, int pageSize = 1000);
        Task<IList<EMBindingInventoryItem>> GetEmBindingInventoryByTerm(string term);
        byte[] DownloadEmBindingInventory();
        byte[] DownloadEmBindingInventoryByTerm(object term);
    }

    public class CustomInventoryService : ICustomInventoryService
    {
        private readonly DSMStorageDataContext _context;

        public CustomInventoryService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IList<NetworkInventoryItem>> GetNetworkInventory()
        {
            return await _context.NetworkInventoryItems.ToListAsync();
        }

        public async Task<IList<NetworkSecurityInventoryItem>> GetNetworkSecurityInventory()
        {
            return await _context.NetworkSecurityInventoryItems.ToListAsync();
        }

        public async Task<IList<FrameworkVersionInventoryItem>> GetFrameworkVersionInventory()
        {
            return await _context.FrameworkVersionInventoryItems.ToListAsync();
        }

        public async Task<IList<EMBindingInventoryItem>> GetEmBindingInventory(int page, int pageSize = 1000)
        {
            return await _context.EmBindingInventoryItems
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IList<EMBindingInventoryItem>> GetEmBindingInventoryByTerm(string term)
        {
            return await _context.EmBindingInventoryItems
                .Where(x => x.SiteName.Contains(term) || x.Host.Contains(term) || x.IPAddress.Contains(term) ||
                            x.State.Contains(term) || x.Bindings.Contains(term))
                .Take(2000)
                .ToListAsync();
        }


        public byte[] DownloadEmBindingInventory()
        {
            return DownloadEmBindingInventoryByTerm(null);
        }

        public byte[] DownloadEmBindingInventoryByTerm(object term)
        {
            IEnumerable<EMBindingInventoryItem> results = null;
            if (term == null)
                results = _context.EmBindingInventoryItems.ToList();
            else
            {
                IEnumerable<PropertyInfo> stringProperties =
                    typeof(EMBindingInventoryItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.EmBindingInventoryItems select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList();
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}