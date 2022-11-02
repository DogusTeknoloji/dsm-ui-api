using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers.CustomHelpers;
using DSM.UI.Api.Models.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Services
{
    public interface ICustomService
    {
        Task<IList<SentryListItem>> GetSentryListItemsAsync();
        Task<SentryListItem> GetSentryListItemAsync(int id);
        Task<SentryListItem> GetTodaySentryAsync();
    }

    public class CustomService : ICustomService
    {
        private readonly DSMStorageDataContext _context;

        public CustomService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IList<SentryListItem>> GetSentryListItemsAsync()
        {
            return await _context.SentryListItems.ToListAsync();
        }

        public async Task<SentryListItem> GetSentryListItemAsync(int id)
        {
            return await _context.SentryListItems.FindAsync(id);
        }

        public async Task<SentryListItem> GetTodaySentryAsync()
        {
            var today = DateTime.Today;

            var monthInTurkish = today.Month.convertMonthToTurkish();

            return await _context.SentryListItems.FirstOrDefaultAsync(x =>
                x.Month.Contains(monthInTurkish) && x.DayNumber == today.Day && x.Year == today.Year);
        }
    }
}