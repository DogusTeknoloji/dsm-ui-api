using DSM.UI.Api.Models.Reports;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public interface IReportsService
    {
        IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber);
    }
    public class ReportsService : IReportsService
    {
        private DSMStorageDataContext _context;
        public ReportsService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber)
        {
            int pageItemCount = 100;
            string numberFormat = "{0:#,#}";
            if (pagenumber < 2)
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                    .ThenBy(x => x.Server.MachineName)
                    .Take(pageItemCount)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.Id,
                        ServerName = x.Server.MachineName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(numberFormat, x.Capacity - x.FreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    }).AsEnumerable();
                return query.Distinct();
            }
            else
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                    .ThenBy(x => x.Server.MachineName)
                    .Skip((pagenumber - 1) * pageItemCount)
                    .Take(pageItemCount)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.Id,
                        ServerName = x.Server.MachineName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(numberFormat, x.Capacity - x.FreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    }).AsEnumerable();
                return query.Distinct();
            }
        }
    }
}
