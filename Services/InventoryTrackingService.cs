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
        #region Updated Site Inventory Tracking

        Task<IEnumerable<UpdatedSiteInventoryItem>> GetAllSiteInventoryAsync();
        Task<UpdatedSiteInventoryItem> GetSiteInventoryItemAsync(int id);

        Task<UpdatedSiteInventoryItem> AddUpdatedSiteInventoryItemAsync(
            UpdatedSiteInventoryItem UpdatedSiteInventoryItem);

        Task<UpdatedSiteInventoryItem> UpdateUpdatedSiteInventoryItemAsync(
            UpdatedSiteInventoryItem UpdatedSiteInventoryItem);

        Task<bool> DeleteUpdatedSiteInventoryItemAsync(int id);
        byte[] DownloadUpdatedSiteInventory();
        byte[] DownloadUpdatedSiteInventory(object term);

        #endregion
        
        # region DetailedServerListR 
        Task<IEnumerable<DetailedServerInventoryItem>> GetAllDetailedServerInventoryAsync();
        Task<DetailedServerInventoryItem> GetDetailedServerInventoryItemAsync(int id);

        Task<DetailedServerInventoryItem> AddDetailedServerInventoryAsync(
            DetailedServerInventoryItem detailedServerInventoryItem);

        Task<DetailedServerInventoryItem> UpdateDetailedServerInventoryAsync(
            DetailedServerInventoryItem detailedServerInventoryItem);

        Task<bool> DeleteDetailedServerInventoryAsync(int id);
        byte[] DownloadDetailedServerInventory();
        byte[] DownloadDetailedServerInventory(object term);

        # endregion
    }

    public class InventoryTrackingService : IInventoryTrackingService
    {
        private readonly DSMStorageDataContext _context;

        public InventoryTrackingService(DSMStorageDataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<UpdatedSiteInventoryItem>> GetAllSiteInventoryAsync()
        {
            return await _context.UpdatedSiteInventoryItems.AsNoTracking().ToListAsync();
        }

        public async Task<UpdatedSiteInventoryItem> GetSiteInventoryItemAsync(int id)
        {
            return await _context.UpdatedSiteInventoryItems.FindAsync(id);
        }

        public async Task<UpdatedSiteInventoryItem> AddUpdatedSiteInventoryItemAsync(
            UpdatedSiteInventoryItem UpdatedSiteInventoryItem)
        {
            _context.UpdatedSiteInventoryItems.Add(UpdatedSiteInventoryItem);
            await _context.SaveChangesAsync();
            return UpdatedSiteInventoryItem;
        }

        public async Task<UpdatedSiteInventoryItem> UpdateUpdatedSiteInventoryItemAsync(
            UpdatedSiteInventoryItem UpdatedSiteInventoryItem)
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

        public async Task<IEnumerable<DetailedServerInventoryItem>> GetAllDetailedServerInventoryAsync()
        {
            return await _context.DetailedServerInventoryItems.AsNoTracking().ToListAsync();
        }

        public async Task<DetailedServerInventoryItem> GetDetailedServerInventoryItemAsync(int id)
        {
            return await _context.DetailedServerInventoryItems.FindAsync(id);
        }

        public async Task<DetailedServerInventoryItem> AddDetailedServerInventoryAsync(DetailedServerInventoryItem detailedServerInventoryItem)
        {
            _context.DetailedServerInventoryItems.Add(detailedServerInventoryItem);
            await _context.SaveChangesAsync();
            return detailedServerInventoryItem;
        }

        public async Task<DetailedServerInventoryItem> UpdateDetailedServerInventoryAsync(DetailedServerInventoryItem detailedServerInventoryItem)
        {
            if (!_context.DetailedServerInventoryItems.Any(x => x.Id == detailedServerInventoryItem.Id))
                return null;

            _context.DetailedServerInventoryItems.Update(detailedServerInventoryItem);
            await _context.SaveChangesAsync();
            return detailedServerInventoryItem;
        }

        public async Task<bool> DeleteDetailedServerInventoryAsync(int id)
        {
            var deletedDetailedSiteInventoryItem = await _context.DetailedServerInventoryItems.FindAsync(id);

            if (deletedDetailedSiteInventoryItem == null)
                return false;

            _context.DetailedServerInventoryItems.Remove(deletedDetailedSiteInventoryItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public byte[] DownloadDetailedServerInventory()
        {
            return DownloadDetailedServerInventory(null);
        }

        public byte[] DownloadDetailedServerInventory(object term)
        {
            IEnumerable<DetailedServerInventoryItem> results = null;
            if (term == null)
            {
                var query = _context.DetailedServerInventoryItems.AsQueryable();
                results = query.ToList().Select(
                    x => new DetailedServerInventoryItem
                    {
                        Id = x.Id,
                        ServerName = x.ServerName,
                        OS = x.OS,
                        IP = x.IP,
                        Responsible = x.Responsible,
                        Server = x.Server,
                        Application = x.Application
                    }
                );
            }
            else
            {
                var query = _context.DetailedServerInventoryItems.AsQueryable();
                var stringProperties = typeof(DetailedServerInventoryItem).GetProperties()
                    .Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, stringProperties, term.ToString());

                results = query.ToList().Select(
                    x => new DetailedServerInventoryItem
                    {
                        Id = x.Id,
                        ServerName = x.ServerName,
                        OS = x.OS,
                        IP = x.IP,
                        Responsible = x.Responsible,
                        Server = x.Server,
                        Application = x.Application
                    }
                );
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}