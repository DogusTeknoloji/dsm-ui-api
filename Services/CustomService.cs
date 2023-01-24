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
        Task<IList<SentryListItem>> GetSentryListItemsAsync(int pageNumber);
        Task<SentryListItem> GetSentryListItemAsync(int id);
        Task<SentryListItem> GetTodaySentryAsync();
        Task<IList<SentryListItem>> GetSentryWithTimeRangeAsync(int MonthRange);
    }

    public class CustomService : ICustomService
    {
        private readonly DSMStorageDataContext _context;
        private const int _pageItemCount = 500;

        public CustomService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IList<SentryListItem>> GetSentryListItemsAsync(int pageNumber)
        {
            var sentryListItems = await _context.SentryListItems
                .OrderByDescending(s => s.Id)
                .Skip((pageNumber - 1) * _pageItemCount)
                .Take(_pageItemCount)
                .ToListAsync();

            return sentryListItems;
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

        public async Task<IList<SentryListItem>> GetSentryWithTimeRangeAsync(int MonthRange)
        {
            var today = DateTime.Today;
            
            var MonthNames = new List<string>();
            
            for (var i = 0; i < MonthRange; i++)
            {
                if(today.Month + i <= 12)
                    MonthNames.Add(today.AddMonths(i).Month.convertMonthToTurkish());
                
                if (today.Month - i >= 0)
                    MonthNames.Add(today.AddMonths(-i).Month.convertMonthToTurkish());
            }
            
            return await _context.SentryListItems.Where(x => MonthNames.Distinct().Contains(x.Month) && x.Year == today.Year).ToListAsync();
        }
    }
}