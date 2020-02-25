using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Server;
using System;
using System.Collections.Generic;
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
        IEnumerable<SearchResult> GetServers(int pagenumber);
        IEnumerable<string> GetLetters();
        IEnumerable<SearchResult> GetServersByLetter(string letter, int pagenumber);
        byte[] DownloadServers(object term = null);
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
            Server result = this._context.Servers.Find(id);

            if (result == null) return null;

            int siteCount = 0;
            var siteCountQuery = this._context.Sites.Where(x => x.MachineName == result.MachineName);
            try
            {
                if (siteCountQuery.FirstOrDefault() != null) siteCount = siteCountQuery.Count();
            }
            catch (Exception)
            {
                siteCount = 0;
            }

            var appServer = this._context.ApplicationServers.FirstOrDefault(x => x.ServerName == result.MachineName);

            string noData = "No-Data";
            string comingSoon = "#COMING_SOON#";
            string numberFormat = "{0:#,#}";
            DetailsGeneral dResult = new DetailsGeneral
            {
                CPU = result.NumCPU.ToString() + " Core CPU",
                Domain = result.DnsName,
                IpAddress = result.IpAddress,
                LastBackup = result.LastBackup.ToString(),
                Memory = result.MemoryGB.ToString() + " GB",
                OperatingSystem = result.OperatingSystem,
                SiteCount = siteCount.ToString(),
                WebServer = appServer?.LicenseVersion,
                OnlineSiteCount = siteCount == 0 ? "0" : comingSoon,
                TotalCapacity = result.ServerDisks.Count > 0 ? string.Format(numberFormat, (result.ServerDisks?.Sum(x => x.Capacity))) + " MB" : noData,
                PercentFree = result.ServerDisks.Count > 0 ? ((100 * result.ServerDisks.Sum(x => x.FreeSpace) / result.ServerDisks.Sum(x => x.Capacity))).ToString() + "% (" + string.Format(numberFormat, result.ServerDisks.Sum(x => x.FreeSpace)) + " MB)" : noData,
                LastCheckDate = result.ServerDisks.FirstOrDefault()?.CheckDate.ToString(),
                Volumes = result.ServerDisks.Count > 0 ? string.Join(", ", result.ServerDisks.Select(x => x.DiskName).ToArray()) : noData,
                VolumeDetails = result.ServerDisks.Select(x => new DetailsVolume
                {
                    VolumeName = x.DiskName,
                    TotalCapacity = string.Format(numberFormat, x.Capacity) + " MB",
                    FreeSpace = string.Format(numberFormat, x.FreeSpace) + " MB",
                    UsedSpace = string.Format(numberFormat, x.Capacity - x.FreeSpace) + " MB",
                    FreePercent = string.Format(numberFormat, 100 * x.FreeSpace / x.Capacity) + "%"
                }).ToList()
            };
            return dResult;
        }

        public IEnumerable<DetailsSites> GetDetailsSites(int id)
        {
            Server serverX = this._context.Servers.Find(id);
            if (serverX == null) return null;
            var query = from site in _context.Sites
                        join server in _context.Servers on site.MachineName equals server.MachineName
                        where site.MachineName == serverX.MachineName
                        select new DetailsSites
                        {
                            SiteId = site.SiteId,
                            SiteName = site.Name,
                            State = site.State,
                            Domains = server.DnsName,
                            PhysicalPath = site.PhysicalPath,
                            AppType = site.AppType
                        };

            return query.Distinct();
        }

        public DetailsHeader GetHeader(int id)
        {
            Server server = this._context.Servers.SingleOrDefault(x => x.Id == id);
            if (server == null) return null;
            return new DetailsHeader
            {
                ServerId = server.Id,
                ServerName = server.MachineName,
                Availability = "Available",
                CompanyName = server.Company.Name,
                CompanyId = server.Company.CompanyId
            };
        }

        public IEnumerable<string> GetLetters()
        {
            var firstLetters = this._context.Servers.GroupBy(s => s.MachineName.Substring(0, 1)).Select(x => x.Key.ToUpper()).ToList();
            firstLetters = firstLetters.OrderBy(x => x).ToList();
            firstLetters.Add("Tümü");
            return firstLetters;
        }

        public IEnumerable<SearchResult> GetServers(int pagenumber)
        {
            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = this._context.Servers.Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.Id,
                    CompanyName = x.Company.Name,
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible
                });
                return query.Distinct();
            }
            else
            {
                var query = this._context.Servers.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.Id,
                    CompanyName = x.Company.Name,
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible
                });
                return query.Distinct();
            }
        }

        public IEnumerable<SearchResult> GetServersByLetter(string letter, int pagenumber)
        {
            int pageItemCount = 100;
            var records = this._context.Servers.Where(x => x.MachineName.StartsWith(letter));

            if (pagenumber < 2)
            {
                var query = records.Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.Id,
                    CompanyName = x.Company.Name,
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible
                });
                return query.Distinct();
            }
            else
            {
                var query = records.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    ServerId = x.Id,
                    CompanyName = x.Company.Name,
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible
                }); ;
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
                DnsName = x.DnsName,
                IpAddress = x.IpAddress,
                MachineName = x.MachineName,
                OperatingSystem = x.OperatingSystem,
                Responsible = x.Responsible,
                ServerId = x.Id
            });
        }

        public byte[] DownloadServers(object term = null)
        {
            IEnumerable<SearchResult> results = null;
         if (term==null)
            {
                var query = this._context.Servers;
                results = query.ToList().Select(x => new SearchResult()
                {
                    ServerId = x.Id,
                    CompanyName = x.Company.Name,
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
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
                    DnsName = x.DnsName,
                    IpAddress = x.IpAddress,
                    MachineName = x.MachineName,
                    OperatingSystem = x.OperatingSystem,
                    Responsible = x.Responsible,
                    ServerId = x.Id
                });
            }
            return ExcelOperations.ExportToExcel(results);
           }
    }
}
