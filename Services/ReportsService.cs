using DSM.UI.Api.Models.Reports;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public interface IReportsService
    {
        IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber);
        IEnumerable<ScheduledJobListDTO> GetScheduledJobs(int pagenumber);
    }
    public class ReportsService : IReportsService
    {
        private const int _pageItemCount = 100;
        private DSMStorageDataContext _context;
        public ReportsService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber)
        {
            string numberFormat = "{0:#,#}";
            if (pagenumber < 2)
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                    .ThenBy(x => x.Server.MachineName)
                    .Take(_pageItemCount)
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
                    .Skip((pagenumber - 1) * _pageItemCount)
                    .Take(_pageItemCount)
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

        public IEnumerable<ScheduledJobListDTO> GetScheduledJobs(int pagenumber)
        {
            if (pagenumber < 2)
            {
                var query = this._context.ScheduledJobItems
                    .Take(_pageItemCount)
                    .Select(x => new ScheduledJobListDTO
                    {
                        HostName = x.Host,
                        HostType = x.HostType,
                        Interval = x.Interval,
                        JobDescription = x.Title,
                        JobName = x.ObjectName,
                        Owner = x.Owner,
                        RepeatTime = x.Schedule,
                        StartTime = x.StartTime
                    }).AsEnumerable();
                return query.Distinct();
            }
            else
            {
                var query = this._context.ScheduledJobItems
                    .Skip((pagenumber - 1) * _pageItemCount)
                    .Take(_pageItemCount)
                    .Select(x => new ScheduledJobListDTO
                    {
                        HostName = x.Host,
                        HostType = x.HostType,
                        Interval = x.Interval,
                        JobDescription = x.Title,
                        JobName = x.ObjectName,
                        Owner = x.Owner,
                        RepeatTime = x.Schedule,
                        StartTime = x.StartTime
                    }).AsEnumerable();
                return query.Distinct();
            }
        }
    }
}
