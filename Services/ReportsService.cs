using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Reports;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DSM.UI.Api.Services
{
    public interface IReportsService
    {
        IEnumerable<KPIMetricsView> GetKpiItems(int pagenumber);
        IEnumerable<OverallDiskStatusItem> GetOverallDiskStatus(int pagenumber);
        IEnumerable<OverallDiskStatusItem> SearchOverallDiskStatus(object term);
        byte[] DownloadOverallDiskStatus();
        byte[] DownloadOverallDiskStatus(object term);
        IEnumerable<ScheduledJobListDTO> GetScheduledJobs(int pagenumber);
        IEnumerable<ScheduledJobListDTO> SearchScheduledJobList(object term);
        byte[] DownloadScheduledJobList();
        byte[] DownloadScheduledJobList(object term);
        IEnumerable<ODMItem> GetODMItems(int pagenumber);
        IEnumerable<SearchODMItemResult> GetSearchODMItems(object term);
        byte[] DownloadODMItems();
        byte[] DownloadODMItems(object term);
    }
    public class ReportsService : IReportsService
    {
        private const int _pageItemCount = 100;
        private readonly DSMStorageDataContext _context;
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
                    .OrderBy(x => 100 * x.DiskFreeSpace / (float)x.DiskCapacity)
                    .ThenBy(x => x.Server.ServerName)
                    .Take(_pageItemCount)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.ServerId,
                        ServerName = x.Server.ServerName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.DiskCapacity) + " MB",
                        FreeDiskSpace = x.DiskFreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.DiskFreeSpace / (float)x.DiskCapacity) + ")",
                        FreePercentage = 100 * x.DiskFreeSpace / x.DiskCapacity,
                        UsedDiskSpace = string.Format(_numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    }).AsEnumerable();
                return query.Distinct();
            }
            else
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.DiskFreeSpace / (float)x.DiskCapacity)
                    .ThenBy(x => x.Server.ServerName)
                    .Skip((pagenumber - 1) * _pageItemCount)
                    .Take(_pageItemCount)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.ServerId,
                        ServerName = x.Server.ServerName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.DiskCapacity) + " MB",
                        FreeDiskSpace = x.DiskFreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.DiskFreeSpace / (float)x.DiskCapacity) + ")",
                        FreePercentage = 100 * x.DiskFreeSpace / x.DiskCapacity,
                        UsedDiskSpace = string.Format(_numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    }).AsEnumerable();
                return query.Distinct();
            }
        }
        public IEnumerable<OverallDiskStatusItem> SearchOverallDiskStatus(object term)
        {
            IEnumerable<PropertyInfo> stringProperties = typeof(OverallDiskStatusItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.ServerDisks
                    .OrderBy(x => 100 * x.DiskFreeSpace / (float)x.DiskCapacity)
                    .ThenBy(x => x.Server.ServerName)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.ServerId,
                        ServerName = x.Server.ServerName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.DiskCapacity) + " MB",
                        FreeDiskSpace = x.DiskFreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.DiskFreeSpace / (float)x.DiskCapacity) + ")",
                        FreePercentage = 100 * x.DiskFreeSpace / x.DiskCapacity,
                        UsedDiskSpace = string.Format(_numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    })
                        select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results;
        }

        public byte[] DownloadOverallDiskStatus()
        {
            return this.DownloadOverallDiskStatus(null);
        }
        public byte[] DownloadOverallDiskStatus(object term)
        {
            IEnumerable<OverallDiskStatusItem> results = null;
            if (term == null)
            {
                var query = this._context.ServerDisks
                    .OrderBy(x => 100 * x.DiskFreeSpace / (float)x.DiskCapacity)
                    .ThenBy(x => x.Server.ServerName)
                    .Select(x => new OverallDiskStatusItem
                    {
                        CompanyName = x.Server.Company.Name,
                        ServerId = x.Server.ServerId,
                        ServerName = x.Server.ServerName,
                        VolumeName = x.DiskName,
                        DiskCapacity = string.Format(_numberFormat, x.DiskCapacity) + " MB",
                        FreeDiskSpace = x.DiskFreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.DiskFreeSpace / (float)x.DiskCapacity) + ")",
                        FreePercentage = 100 * x.DiskFreeSpace / x.DiskCapacity,
                        UsedDiskSpace = string.Format(_numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
                        Responsible = x.Server.Responsible
                    });
                results = query.ToList();
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(OverallDiskStatusItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.ServerDisks
                        .OrderBy(x => 100 * x.DiskFreeSpace / (float)x.DiskCapacity)
                        .ThenBy(x => x.Server.ServerName)
                        .Select(x => new OverallDiskStatusItem
                        {
                            CompanyName = x.Server.Company.Name,
                            ServerId = x.Server.ServerId,
                            ServerName = x.Server.ServerName,
                            VolumeName = x.DiskName,
                            DiskCapacity = string.Format(_numberFormat, x.DiskCapacity) + " MB",
                            FreeDiskSpace = x.DiskFreeSpace.ToString() + " MB (% " + string.Format("{0:0.##}", 100 * x.DiskFreeSpace / (float)x.DiskCapacity) + ")",
                            FreePercentage = 100 * x.DiskFreeSpace / x.DiskCapacity,
                            UsedDiskSpace = string.Format(_numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
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

        public byte[] DownloadScheduledJobList()
        {
            return this.DownloadScheduledJobList(null);
        }
        public byte[] DownloadScheduledJobList(object term)
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

        public IEnumerable<ODMItem> GetODMItems(int pagenumber)
        {
            var query = from site in this._context.Sites
                        join server in this._context.Servers
                            on site.MachineName equals server.ServerName
                        select new ODMItem
                        {
                            SiteName = site.Name,
                            ServerName = server.ServerName,
                            DnsName = server.HostName,
                            IpAddress = server.IpAddress,
                            OdmStatus = server.OdmReplication,
                            OperatingSystem = server.OperatingSystem,
                            Responsible = server.Responsible,
                            Service = server.ServiceName,
                        };

            if (pagenumber < 2)
            {
                return query.Take(_pageItemCount).AsEnumerable().Distinct();
            }
            else
            {
                return query.Skip((pagenumber - 1) * _pageItemCount).Take(_pageItemCount).AsEnumerable().Distinct();
            }
        }

        public IEnumerable<SearchODMItemResult> GetSearchODMItems(object term)
        {
            IEnumerable<PropertyInfo> stringProperties = typeof(ODMItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from site in this._context.Sites
                        join server in this._context.Servers on site.MachineName equals server.ServerName
                        select new ODMItem
                        {
                            SiteName = site.Name,
                            ServerName = server.ServerName,
                            DnsName = server.HostName,
                            IpAddress = server.IpAddress,
                            OdmStatus = server.OdmReplication,
                            OperatingSystem = server.OperatingSystem,
                            Responsible = server.Responsible,
                            Service = server.ServiceName,
                        };
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());
            var results = query.ToList();

            return results.Select(x => new SearchODMItemResult
            {
                DnsName = x.DnsName,
                IpAddress = x.IpAddress,
                OdmStatus = x.OdmStatus,
                OperatingSystem = x.OperatingSystem,
                Responsible = x.Responsible,
                ServerName = x.ServerName,
                Service = x.Service,
                SiteName = x.SiteName
            });
        }

        public byte[] DownloadODMItems()
        {
            return this.DownloadODMItems(null);
        }

        public byte[] DownloadODMItems(object term)
        {
            IEnumerable<SearchODMItemResult> results = null;
            var query = from site in this._context.Sites
                        join server in this._context.Servers on site.MachineName equals server.ServerName
                        select new ODMItem
                        {
                            SiteName = site.Name,
                            ServerName = server.ServerName,
                            DnsName = server.HostName,
                            IpAddress = server.IpAddress,
                            OdmStatus = server.OdmReplication,
                            OperatingSystem = server.OperatingSystem,
                            Responsible = server.Responsible,
                            Service = server.ServiceName,
                        };

            if (term == null)
            {
                results = query.ToList().Select(x => new SearchODMItemResult
                {
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    OdmStatus = x.OdmStatus,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServerName = x.ServerName,
                    Service = x.Service,
                    SiteName = x.SiteName
                });
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(ODMItem).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList().Select(x => new SearchODMItemResult
                {
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    OdmStatus = x.OdmStatus,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServerName = x.ServerName,
                    Service = x.Service,
                    SiteName = x.SiteName
                });
            }

            return ExcelOperations.ExportToExcel(results);
        }

        public IEnumerable<KPIMetricsView> GetKpiItems(int pagenumber)
        {
            var query = from kpi in this._context.KPIMetricsViews
                        select new KPIMetricsView

                {
                    Application = kpi.Application,
                    Year = kpi.Year,
                    Ocak = kpi.Ocak,
                    Subat = kpi.Subat,
                    Mart = kpi.Mart,
                    Nisan = kpi.Nisan,
                    Mayis = kpi.Mayis,
                    Haziran = kpi.Haziran,
                    Temmuz = kpi.Temmuz,
                    Agustos = kpi.Agustos,
                    Eylul = kpi.Eylul,
                    Ekim = kpi.Ekim,
                    Kasim = kpi.Kasim,
                    Aralik = kpi.Aralik
                };

            if (pagenumber < 2)
            {
                return query.Take(_pageItemCount).AsEnumerable().Distinct();
            }
            else
            {
                return query.Skip((pagenumber - 1) * _pageItemCount).Take(_pageItemCount).AsEnumerable().Distinct();
            }
        }
    }
}
