using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DSM.UI.Api.Services
{
    public interface ISiteService
    {
        IEnumerable<SearchResult> SearchSites(object term);
        DetailsHeader GetHeader(long id);
        DetailsGeneral GetDetailsGeneral(long id);
        IEnumerable<DetailsBinding> GetDetailsBindings(long id);
        IEnumerable<DetailsPackage> GetDetailsPackages(long id);
        IEnumerable<DetailsBackendServiceConnectionString> GetDetailsConnectionStrings(long id);
        IEnumerable<DetailsBackendServiceEndpoint> GetDetailsEndpoint(long id);
        IEnumerable<SearchResult> GetSites(int pagenumber);
        IEnumerable<string> GetLetters();
        IEnumerable<Core.Models.Site> GetSitesByLetter(string letter, int pagenumber);
        byte[] DownloadSites(object term = null);
    }
    public class SiteService : ISiteService
    {
        private DSMStorageDataContext _context;
        public SiteService(DSMStorageDataContext context)
        {
            _context = context;
            //context.Database.EnsureCreated();
        }

        public IEnumerable<DetailsBinding> GetDetailsBindings(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;
            if (query.Bindings == null) return new List<DetailsBinding>();

            return query.Bindings.Select(x => new DetailsBinding
            {
                Host = x.Host,
                IpAddress = x.IpAddress,
                IpAddressFamily = x.IpAddressFamily,
                Ports = x.Port,
                Protocols = x.Protocol
            });
        }

        public IEnumerable<DetailsBackendServiceConnectionString> GetDetailsConnectionStrings(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;
            if (query.ConnectionStrings == null) return new List<DetailsBackendServiceConnectionString>();

            return query.ConnectionStrings.Select(x => new DetailsBackendServiceConnectionString
            {
                DatabaseName = x.DatabaseName,
                Port = x.Port.ToString(),
                ServerName = x.ServerName,
                Username = x.UserName,
                ConnectionString = x.RawConnectionString
            });
        }

        public IEnumerable<DetailsBackendServiceEndpoint> GetDetailsEndpoint(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;
            if (query.Endpoints == null) return new List<DetailsBackendServiceEndpoint>();

            return query.Endpoints.Select(x => new DetailsBackendServiceEndpoint
            {
                EndpointName = x.EndpointName,
                EndpointUrl = x.EndpointUrl,
                IsAvailable = x.IsAvailable ? "Yes" : "No",
                LastCheckDate = x.LastCheckDate.ToString(),
                Port = x.Port.ToString()
            });
        }

        public DetailsGeneral GetDetailsGeneral(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;

            return new DetailsGeneral
            {
                ApplicationPoolName = query.ApplicationPoolName,
                AppType = query.AppType,
                DateDeleted = query.DateDeleted.ToString(),
                EnabledProtocols = query.EnabledProtocols,
                LastUpdated = query.LastUpdated.ToString(),
                LogFileDirectory = query.LogFileDirectory,
                LogFileEnabled = query.LogFileEnabled ? "Yes" : "No",
                LogFormat = query.LogFormat,
                LogPeriod = query.LogPeriod,
                MachineName = query.MachineName,
                MaxBandwidth = query.MaxBandwitdh.ToString(),
                MaxConnections = query.MaxConnections.ToString(),
                Name = query.Name,
                NetFrameworkVersion = query.Packages?.SingleOrDefault(x => x.Name == "NetFrameworkVersion")?.Version,
                PhysicalPath = query.PhysicalPath,
                SendAlertMAilWhenUnavailable = query.SendAlertMailWhenUnavailable ? "Yes" : "No",
                ServerAutoStart = query.ServerAutoStart ? "Yes" : "No",
                TraceFailedRequestsLoggingEnabled = query.TraceFailedRequestsLoggingEnabled ? "Yes" : "No",
                WebcConfigBackupDirectory = query.WebConfigBackupDirectory,
                WebConfigLastBackupDate = query.WebConfigLastBackupDate.ToString()
            };
        }

        public IEnumerable<DetailsPackage> GetDetailsPackages(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;
            if (query.Packages == null) return new List<DetailsPackage>();

            return query.Packages.Select(x => new DetailsPackage
            {
                Name = x.Name,
                Version = x.Version
            });
        }

        private string CalculateTime(DateTime datetime)
        {
            var result = DateTime.Now - datetime;
            if (result.TotalSeconds < 60) return result.TotalSeconds.ToString("F0") + " sec";
            if (result.TotalMinutes < 60) return result.TotalMinutes.ToString("F0") + "min";
            if (result.TotalHours < 60) return result.TotalHours.ToString("F0") + " hour";
            return result.TotalDays.ToString("F0") + " days";
        }
        public DetailsHeader GetHeader(long id)
        {
            var query = _context.Sites.SingleOrDefault(x => x.SiteId == id);
            if (query == null) return null;

            var companyQuery = _context.Servers.FirstOrDefault(x => x.ServerName == query.MachineName);
            if (companyQuery == null) return null;

            DetailsHeader header = new DetailsHeader
            {
                SiteName = query.Name,
                MachineId = companyQuery?.ServerId,
                MachineName = query.MachineName,
                Availability = query.IsAvailable ? "Available" : "Not Available",
                LastUpdated = CalculateTime(query.LastUpdated) + " ago",
                State = query.State,
                CompanyId = companyQuery.Company?.CompanyId,
                CompanyName = companyQuery.Company?.Name
            };
            return header;
        }

        public IEnumerable<SearchResult> SearchSites(object term)
        {
            object queryItem = term;
            IEnumerable<PropertyInfo> stringProperties = typeof(Site).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.Sites select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList().Select(x => new SearchResult
            {
                AppPoolName = x.ApplicationPoolName,
                AppType = x.AppType,
                IpAddress = x.Bindings.FirstOrDefault()?.IpAddress,
                HostName = x.Bindings.FirstOrDefault()?.Host,
                LogFileDirectory = x.LogFileDirectory,
                Machinename = x.MachineName,
                PhysicalPath = x.PhysicalPath,
                Port = x.Bindings.FirstOrDefault()?.Port,
                SiteId = x.SiteId,
                SiteName = x.Name
            });

            return results;
        }

        public IEnumerable<SearchResult> GetSites(int pagenumber)
        {
            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = this._context.Sites.Take(pageItemCount).Select(x => new SearchResult
                {
                    AppPoolName = x.ApplicationPoolName,
                    AppType = x.AppType,
                    IpAddress = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().IpAddress,
                    HostName = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().Host,
                    LogFileDirectory = x.LogFileDirectory,
                    Machinename = x.MachineName,
                    PhysicalPath = x.PhysicalPath,
                    Port = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().Port,
                    SiteId = x.SiteId,
                    SiteName = x.Name
                });
                return query;
            }
            else
            {
                var query = this._context.Sites.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult
                {
                    AppPoolName = x.ApplicationPoolName,
                    AppType = x.AppType,
                    IpAddress = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().IpAddress,
                    HostName = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().Host,
                    LogFileDirectory = x.LogFileDirectory,
                    Machinename = x.MachineName,
                    PhysicalPath = x.PhysicalPath,
                    Port = x.Bindings.FirstOrDefault() == null ? null : x.Bindings.FirstOrDefault().Port,
                    SiteId = x.SiteId,
                    SiteName = x.Name
                });
                return query;
            }
        }

        public IEnumerable<string> GetLetters()
        {
            var firstLetters = this._context.Sites.GroupBy(s => s.Name.Substring(0, 1)).Select(x => x.Key.ToUpper()).ToList();
            firstLetters.Add("Tümü");
            return firstLetters;
        }

        public IEnumerable<Core.Models.Site> GetSitesByLetter(string letter, int pagenumber = 1)
        {
            int pageItemCount = 100;
            var records = this._context.Sites.Where(x => x.Name.StartsWith(letter));

            if (pagenumber < 2)
            {
                var query = records.Take(pageItemCount);
                return query;
            }
            else
            {
                var query = records.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount);
                return query;
            }
        }

        public byte[] DownloadSites(object term = null)
        {
            IEnumerable<SearchResult> results = null;
            object queryItem = term;
            if (term == null)
            {
                var query = this._context.Sites;
                results = query.ToList().Select(x => new SearchResult
                {
                    AppPoolName = x.ApplicationPoolName,
                    AppType = x.AppType,
                    IpAddress = x.Bindings.FirstOrDefault()?.IpAddress,
                    HostName = x.Bindings.FirstOrDefault()?.Host,
                    LogFileDirectory = x.LogFileDirectory,
                    Machinename = x.MachineName,
                    PhysicalPath = x.PhysicalPath,
                    Port = x.Bindings.FirstOrDefault()?.Port,
                    SiteId = x.SiteId,
                    SiteName = x.Name
                });
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(Site).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.Sites select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

                results = query.ToList().Select(x => new SearchResult
                {
                    AppPoolName = x.ApplicationPoolName,
                    AppType = x.AppType,
                    IpAddress = x.Bindings.FirstOrDefault()?.IpAddress,
                    HostName = x.Bindings.FirstOrDefault()?.Host,
                    LogFileDirectory = x.LogFileDirectory,
                    Machinename = x.MachineName,
                    PhysicalPath = x.PhysicalPath,
                    Port = x.Bindings.FirstOrDefault()?.Port,
                    SiteId = x.SiteId,
                    SiteName = x.Name
                });
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}