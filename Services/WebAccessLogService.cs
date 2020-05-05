using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.WebAccessLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Services
{
    public interface IWebAccessLogService
    {
        Task WriteLogAsync(WebAccessLog logData);
        IEnumerable<WebAccessLog> GetLogsByDate(DateTime startDate, DateTime endDate);
        IEnumerable<WebAccessLog> GetLogsByUserName(string userName);
    }
    public class WebAccessLogService : IWebAccessLogService
    {
        private readonly DSMAuthDbContext _context;
        public WebAccessLogService(DSMAuthDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WebAccessLog> GetLogsByDate(DateTime startDate, DateTime endDate)
        {
            IEnumerable<WebAccessLog> results = this._context.WebAccessLogs.Where(x => x.LogTimeStamp >= startDate && x.LogTimeStamp <= endDate);
            return results;
        }

        public IEnumerable<WebAccessLog> GetLogsByUserName(string userName)
        {
            IEnumerable<WebAccessLog> results = this._context.WebAccessLogs.Where(x => x.UserName.ToLowerInvariant() == userName.ToLowerInvariant());
            return results;
        }

        public async Task WriteLogAsync(WebAccessLog logData)
        {
            EntityEntry<WebAccessLog> result = await this._context.AddAsync(logData);
            if (result.State == EntityState.Added)
            {
                _ = await this._context.SaveChangesAsync();
            }
        }
    }
}