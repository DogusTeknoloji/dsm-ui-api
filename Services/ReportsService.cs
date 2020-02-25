using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Reports;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DSM.UI.Api.Services
{
    public interface IReportsService
    {
        IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber);
        IEnumerable<OverallDiskStatusItem> SearchOverallDiskStatus(object term);
        byte[] DownloadOverallDiskStatus(object term = null);
        IEnumerable<ScheduledJobListDTO> GetScheduledJobs(int pagenumber);
        IEnumerable<ScheduledJobListDTO> SearchScheduledJobList(object term);
        byte[] DownloadScheduledJobList(object term = null);

    }
    public class ReportsService : IReportsService
    {
        private const int _pageItemCount = 100;
        private DSMStorageDataContext _context;
        private const string _numberFormat = "{0:#,#}";
        public ReportsService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber)
        {
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
                        DiskCapacity = string.Format(_numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(_numberFormat, x.Capacity - x.FreeSpace) + " MB",
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
                        DiskCapacity = string.Format(_numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(_numberFormat, x.Capacity - x.FreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    }).AsEnumerable();
                return query.Distinct();
            }
        }

        public IEnumerable<OverallDiskStatusItem> SearchOverallDiskStatus(object term)
        {
            object queryItem = term;
            IEnumerable<PropertyInfo> stringProperties = typeof(OverallDiskStatusItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.ServerDisks
                    .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                    .ThenBy(x => x.Server.MachineName)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.Id,
                        ServerName = x.Server.MachineName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(_numberFormat, x.Capacity - x.FreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    })
                        select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results;
        }

        public byte[] DownloadOverallDiskStatus(object term = null)
        {
            IEnumerable<OverallDiskStatusItem> results = null;
            if (term == null)
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                    .ThenBy(x => x.Server.MachineName)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.Id,
                        ServerName = x.Server.MachineName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.Capacity) + " MB",
                        FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                        FreePercentage = 100 * x.FreeSpace / x.Capacity,
                        LastCheckDate = x.CheckDate,
                        UsedDiskSpace = string.Format(_numberFormat, x.Capacity - x.FreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    });
                results = query.ToList();
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(OverallDiskStatusItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.ServerDisks
                        .OrderBy(x => 100 * x.FreeSpace / (float)x.Capacity)
                        .ThenBy(x => x.Server.MachineName)
                        .Select(x => new OverallDiskStatusItem
                        {
                            CompanyName = x.Server.Company.Name,
                            ServerId = x.Server.Id,
                            ServerName = x.Server.MachineName,
                            VolumeName = x.DiskName,
                            DiskCapacity = string.Format(_numberFormat, x.Capacity) + " MB",
                            FreeDiskSpace = x.FreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.FreeSpace / (float)x.Capacity) + ")",
                            FreePercentage = 100 * x.FreeSpace / x.Capacity,
                            LastCheckDate = x.CheckDate,
                            UsedDiskSpace = string.Format(_numberFormat, x.Capacity - x.FreeSpace) + " MB",
                            Responsible = x.Server.Responsible
                        })
                            select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList();
            }

            return ExcelOperations.ExportToExcel(results);
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

        public IEnumerable<ScheduledJobListDTO> SearchScheduledJobList(object term)
        {
            object queryItem = term;
            IEnumerable<PropertyInfo> stringProperties = typeof(ScheduledJobItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.ScheduledJobItems select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results.Select(x => new ScheduledJobListDTO
            {
                HostName = x.Host,
                HostType = x.HostType,
                Interval = x.Interval,
                JobDescription = x.Title,
                JobName = x.ObjectName,
                Owner = x.Owner,
                RepeatTime = x.Schedule,
                StartTime = x.StartTime
            });
        }

        public byte[] DownloadScheduledJobList(object term = null)
        {
            IEnumerable<ScheduledJobListDTO> results = null;
            if (term == null)
            {
                var query = this._context.ScheduledJobItems
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
                   });
                results = query.ToList();
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(ScheduledJobItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.ScheduledJobItems select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList().Select(x => new ScheduledJobListDTO
                {
                    HostName = x.Host,
                    HostType = x.HostType,
                    Interval = x.Interval,
                    JobDescription = x.Title,
                    JobName = x.ObjectName,
                    Owner = x.Owner,
                    RepeatTime = x.Schedule,
                    StartTime = x.StartTime
                });
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}
