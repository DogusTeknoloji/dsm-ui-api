using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.CustomerUrlLists;
using DSM.UI.Api.Models.EditableInventory;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Services
{
    public interface IInventoryTrackingService
    {
        Task<IEnumerable<UpdatedSiteInventoryItem>> GetAllSiteInventory();
        Task<UpdatedSiteInventoryItem> GetSiteInventoryItem(int id);
        Task<UpdatedSiteInventoryItem> AddUpdatedSiteInventoryItemAsync(UpdatedSiteInventoryItem UpdatedSiteInventoryItem);
        Task<UpdatedSiteInventoryItem> UpdateUpdatedSiteInventoryItemAsync(UpdatedSiteInventoryItem UpdatedSiteInventoryItem);
        Task<bool> DeleteUpdatedSiteInventoryItemAsync(int id);
        byte[] DownloadUpdatedSiteInventory();
        byte[] DownloadUpdatedSiteInventory(object term);
    }
    public class InventoryTrackingService : IInventoryTrackingService
    {
        private readonly DSMStorageDataContext _context;

        public InventoryTrackingService(DSMStorageDataContext context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<UpdatedSiteInventoryItem>> GetAllSiteInventory()
        {
            return await _context.UpdatedSiteInventoryItems.AsNoTracking().ToListAsync();
        }

        public async Task<UpdatedSiteInventoryItem> GetSiteInventoryItem(int id)
        {
            return await _context.UpdatedSiteInventoryItems.FindAsync(id);
        }

        public async Task<UpdatedSiteInventoryItem> AddUpdatedSiteInventoryItemAsync(UpdatedSiteInventoryItem UpdatedSiteInventoryItem)
        {
            _context.UpdatedSiteInventoryItems.Add(UpdatedSiteInventoryItem);
            await _context.SaveChangesAsync();
            return UpdatedSiteInventoryItem;
        }

        public async Task<UpdatedSiteInventoryItem> UpdateUpdatedSiteInventoryItemAsync(UpdatedSiteInventoryItem UpdatedSiteInventoryItem)
        {
            if (!_context.UpdatedSiteInventoryItems.Any(x => x.Id == UpdatedSiteInventoryItem.Id))
                return null;

            _context.UpdatedSiteInventoryItems.Update(UpdatedSiteInventoryItem);
            await _context.SaveChangesAsync();
            return UpdatedSiteInventoryItem;
        }

        public async Task<bool> DeleteUpdatedSiteInventoryItemAsync(int id)
        {
            var updatedSiteInventoryItem = await _context.UpdatedSiteInventoryItems.FindAsync(id);

            if (updatedSiteInventoryItem == null)
                return false;

            _context.UpdatedSiteInventoryItems.Remove(updatedSiteInventoryItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public byte[] DownloadUpdatedSiteInventory()
        {
            return DownloadUpdatedSiteInventory(null);
        }

        public byte[] DownloadUpdatedSiteInventory(object term)
        {
            IEnumerable<UpdatedSiteInventoryItem> results = null;
            if (term == null)
            {
                var query = _context.UpdatedSiteInventoryItems.AsQueryable();
                results = query.ToList().Select(
                    x => new UpdatedSiteInventoryItem
                    {
                        Id = x.Id,
                        MachineName = x.MachineName,
                        SiteName = x.SiteName,
                        Company = x.Company,
                        Responsible = x.Responsible,
                        Owner = x.Owner
                    }
                );
            }
            else
            {
                var query = _context.UpdatedSiteInventoryItems.AsQueryable();
                var stringProperties = typeof(UpdatedSiteInventoryItem).GetProperties()
                    .Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, stringProperties, term.ToString());

                results = query.ToList().Select(
                    x => new UpdatedSiteInventoryItem
                    {
                        Id = x.Id,
                        MachineName = x.MachineName,
                        SiteName = x.SiteName,
                        Company = x.Company,
                        Responsible = x.Responsible,
                        Owner = x.Owner
                    }
                );
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}