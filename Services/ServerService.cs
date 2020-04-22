using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.RemoteDesktop;
using DSM.UI.Api.Helpers.RemoteDesktop.Models;
using DSM.UI.Api.Models.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace DSM.UI.Api.Services
{
    public interface IServerService
    {
        IEnumerable<SearchResult> SearchServers(object term);
        DetailsHeader GetHeader(int id);
        DetailsGeneral GetDetailsGeneral(int id);
        IEnumerable<DetailsSites> GetDetailsSites(int id);
        IEnumerable<SearchResult> GetServers(int pagenumber, string fieldName = null, int orderPosition = -1);
        IEnumerable<string> GetLetters();
        IEnumerable<SearchResult> GetServersByLetter(string letter, int pagenumber);
        byte[] DownloadServers(object term = null);
        byte[] DownloadRDPFile(RdpInfo rdpInfo);
        string GetServerCheckDate();
    }

    public class ServerService : IServerService
    {
        private DSMStorageDataContext _context;
        public ServerService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public DetailsGeneral GetDetailsGeneral(int id)
        {
            Server result = this._context.Servers.FirstOrDefault(x => x.ServerId == id);

            if (result == null) return null;

            int siteCount = 0;
            var siteCountQuery = this._context.Sites.Where(x => x.MachineName == result.ServerName);
            try
            {
                if (siteCountQuery.FirstOrDefault() != null) siteCount = siteCountQuery.Count();
            }
            catch (Exception)
            {
                siteCount = 0;
            }

            string lastCheckDate = this._context.VCenterLogs.Where(x => x.LogName == "DiskStatus").Select(x => x.LogValue).FirstOrDefault();
            lastCheckDate = Convert.ToDateTime(lastCheckDate).ToString();
            string noData = "No-Data";
            string comingSoon = "#COMING_SOON#";
            string numberFormat = "{0:#,#}";
            DetailsGeneral dResult = new DetailsGeneral
            {
                CPU = result.TotalCPU.ToString() + " Core CPU",
                Domain = result.HostName,
                IpAddress = result.IpAddress,
                LastBackup = result.LastBackup == null ? "No-Backup" : result.LastBackup.ToString(),
                Memory = result.TotalMemory.ToString() + " MB",
                OperatingSystem = result.OperatingSystem,
                SiteCount = siteCount.ToString(),
                Boottime = result.Boottime.ToString(),
                CustomIp = result.CustomIp,
                MemoryUsage = result.MemoryUsage.ToString() + " MB",
                Notes = result.Notes,
                PhysicalLocation = result.PhysicalLocation,
                Responsible = result.Responsible,
                ServerType = result.ServerType,
                ServiceName = result.ServiceName,
                ToolsRunningStatus = result.ToolsRunningStatus,
                OnlineSiteCount = siteCount == 0 ? "0" : comingSoon,
                TotalCapacity = result.ServerDisks.Count > 0 ? string.Format(numberFormat, (result.ServerDisks?.Sum(x => x.DiskCapacity))) + " MB" : noData,
                PercentFree = result.ServerDisks.Count > 0 ? ((100 * result.ServerDisks.Sum(x => x.DiskFreeSpace) / result.ServerDisks.Sum(x => x.DiskCapacity))).ToString() + "% (" + string.Format(numberFormat, result.ServerDisks.Sum(x => x.DiskFreeSpace)) + " MB)" : noData,
                Volumes = result.ServerDisks.Count > 0 ? string.Join(", ", result.ServerDisks.Select(x => x.DiskName).ToArray()) : noData,
                LastCheckDate = lastCheckDate,
                VolumeDetails = result.ServerDisks.Select(x => new DetailsVolume
                {
                    VolumeName = x.DiskName,
                    TotalCapacity = string.Format(numberFormat, x.DiskCapacity) + " MB",
                    FreeSpace = string.Format(numberFormat, x.DiskFreeSpace) + " MB",
                    UsedSpace = string.Format(numberFormat, x.DiskCapacity - x.DiskFreeSpace) + " MB",
                    FreePercent = string.Format(numberFormat, 100 * x.DiskFreeSpace / x.DiskCapacity) + "%"
                }).ToList()
            };
            return dResult;
        }

        public IEnumerable<DetailsSites> GetDetailsSites(int id)
        {
            Server serverX = this._context.Servers.Find(id);
            if (serverX == null) return null;
            var query = from site in _context.Sites
                        join server in _context.Servers on site.MachineName equals server.ServerName
                        where site.MachineName == serverX.ServerName
                        select new DetailsSites
                        {
                            SiteId = site.SiteId,
                            SiteName = site.Name,
                            State = site.State,
                            Domains = server.HostName,
                            PhysicalPath = site.PhysicalPath,
                            AppType = site.AppType
                        };

            return query.Distinct();
        }

        public DetailsHeader GetHeader(int id)
        {
            Server server = this._context.Servers.SingleOrDefault(x => x.ServerId == id);
            if (server == null) return null;
            return new DetailsHeader
            {
                ServerId = server.ServerId,
                ServerName = server.ServerName,
                Availability = "Available",
                CompanyName = server.Company.Name,
                CompanyId = server.Company.CompanyId
            };
        }

        public IEnumerable<string> GetLetters()
        {
            var firstLetters = this._context.Servers.GroupBy(s => s.ServerName.Substring(0, 1)).Select(x => x.Key.ToUpper()).ToList();
            firstLetters = firstLetters.OrderBy(x => x).ToList();
            firstLetters.Add("Tümü");
            return firstLetters;
        }

        public IEnumerable<SearchResult> GetServers(int pagenumber, string fieldName = null, int orderPosition = -1)
        {
            var orderQuery = from a in this._context.Servers select a;
            
            if (!(fieldName is null) && orderPosition != -1)
            {
                PropertyInfo orderColumn = typeof(Server).GetProperties().FirstOrDefault(prop => prop.Name.ToLower(CultureInfo.GetCultureInfo("en-US")) == fieldName.ToLower(CultureInfo.GetCultureInfo("en-US")));
                if (orderColumn == null) return null;
                orderQuery = EntityQueryable.OrderBy(orderQuery, orderColumn.Name, orderPosition == 1 ? false : true);
                orderQuery = orderQuery.AsEnumerable().AsQueryable();
            }

            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = orderQuery.Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.ServerId,
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServiceName = x.ServiceName
                });
                return query;
            }
            else
            {
                var query = orderQuery.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.ServerId,
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServiceName = x.ServiceName
                });
                return query;
            }
        }

        public IEnumerable<SearchResult> GetServersByLetter(string letter, int pagenumber)
        {
            int pageItemCount = 100;
            var records = this._context.Servers.Where(x => x.ServerName.StartsWith(letter));

            if (pagenumber < 2)
            {
                var query = records.Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.ServerId,
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServiceName = x.ServiceName
                });
                return query.Distinct();
            }
            else
            {
                var query = records.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.ServerId,
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServiceName = x.ServiceName
                });
                return query.Distinct();
            }
        }

        public IEnumerable<SearchResult> SearchServers(object term)
        {
            object queryItem = term;
            IEnumerable<PropertyInfo> stringProperties = typeof(Server).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.Servers select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results.Select(x => new SearchResult
            {
                CompanyName = x.Company.Name,
                DnsName = x.HostName,
                IpAddress = x.IpAddress,
                MachineName = x.ServerName,
                OperatingSystem = x.OperatingSystem,
                Responsible = x.Responsible,
                ServiceName = x.ServiceName,
                ServerId = x.ServerId
            });
        }

        public byte[] DownloadServers(object term = null)
        {
            IEnumerable<SearchResult> results = null;
            if (term == null)
            {
                var query = this._context.Servers;
                results = query.ToList().Select(x => new SearchResult()
                {
                    ServerId = x.ServerId,
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible
                });
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(Server).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.Servers select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList().Select(x => new SearchResult
                {
                    CompanyName = x.Company.Name,
                    DnsName = x.HostName,
                    IpAddress = x.IpAddress,
                    MachineName = x.ServerName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServerId = x.ServerId
                });
            }
            return ExcelOperations.ExportToExcel(results);
        }

        public byte[] DownloadRDPFile(RdpInfo rdpInfo)
        {
            string ipAddress = this._context.Servers
                .Where(x => x.ServerId == rdpInfo.ServerId)
                .Select(x => x.IpAddress)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(ipAddress)) return null;

            RDPManager.rdpLoginInfo.IpAddress = ipAddress;
            RDPManager.rdpLoginInfo.Username = rdpInfo.UserName;
            byte[] result = RDPManager.CreateFile(RDPManager.rdpLoginInfo);
            return result;
        }

        public string GetServerCheckDate()
        {
            var query = from a in _context.VCenterLogs where a.LogName == "ServerInventory" select a.LogValue;
            string checkDate = query.FirstOrDefault();
            checkDate = Convert.ToDateTime(checkDate).ToString();
            return checkDate;
        }
    }
}
