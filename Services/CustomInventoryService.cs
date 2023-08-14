using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers.CustomHelpers;
using DSM.UI.Api.Models.CustomInventories;
using DSM.UI.Api.Models.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Services
{
    public interface ICustomInventoryService
    {
        Task<IList<NetworkInventoryItem>> GetNetworkInventory();
        Task<IList<NetworkSecurityInventoryItem>> GetNetworkSecurityInventory();
        
        Task<IList<FrameworkVersionInventoryItem>> GetFrameworkVersionInventory();
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
    }
}