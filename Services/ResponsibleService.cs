using System.Collections.Generic;
using System.Linq;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CompanyComparer;
using DSM.UI.Api.Models.Company;
using DSM.UI.Api.Models.Responsible;
using DSM.UI.Api.Models.Server;
using DetailsSites = DSM.UI.Api.Models.Company.DetailsSites;

namespace DSM.UI.Api.Services
{
    public interface IResponsibleService
    {
        IEnumerable<RespSearchResult> GetResponsibles();
        IEnumerable<RespSearchResult> SearchResponsibles(object term);
        IEnumerable<DetailsServers> GetDetailsServers(string responsibleName);
        int GetResponsibleServerCount(string responsibleName);
        int GetResponsibleSiteCount(string responsibleName);
        IEnumerable<DetailsSites> GetDetailsSites(string responsibleName);
        public byte[] DownloadResponsibles();
        public byte[] DownloadResponsibles(object term);
    }

    public class ResponsibleService : IResponsibleService
    {
        private readonly DSMStorageDataContext _context;

        public ResponsibleService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public IEnumerable<RespSearchResult> SearchResponsibles(object term)
        {
            var stringProperties = typeof(Server).GetProperties().Where(pr => pr.Name == "Responsible");

            var query = from s in _context.Servers select s;

            query = query.WhereContains(stringProperties, term.ToString());

            return query
                .ToList()
                .Select(r => r.Responsible)
                .Distinct()
                .Select(responsibleName => new RespSearchResult
                {
                    ResponsibleName = responsibleName,
                    CountOfServers = GetResponsibleServerCount(responsibleName),
                    CountOfSites = GetResponsibleSiteCount(responsibleName)
                });
        }

        public int GetResponsibleServerCount(string responsibleName)
        {
            return _context.Servers.Count(s => s.Responsible == responsibleName);
        }

        public int GetResponsibleSiteCount(string responsibleName)
        {
            return (from server in _context.Servers
                join site in _context.Sites
                    on server.ServerName.ToUpper() equals site.MachineName.ToUpper()
                where server.Responsible == responsibleName
                select site).Count();
        }

        public IEnumerable<DetailsServers> GetDetailsServers(string responsibleName)
        {
            var query = from server in _context.Servers.ToList()
                where server.Responsible == responsibleName
                select new DetailsServers
                {
                    ServerId = server.ServerId,
                    ApplicationType = server.ServiceName,
                    ServerName = server.ServerName,
                    Contact = server.Responsible,
                    Environments = server.ServerType,
                    FullName = server.HostName,
                    IpAddress = server.IpAddress,
                    LastBackupDate = server.LastBackup.ToString(),
                    OperatingSystem = server.OperatingSystem,
                    Owner = GetServerCompany(server.CompanyId)
                };

            return query.Distinct();
        }

        public IEnumerable<DetailsSites> GetDetailsSites(string responsibleName)
        {
            var query = from server in _context.Servers.ToList()
                join site in _context.Sites
                    on server.ServerName.ToUpper() equals site.MachineName.ToUpper()
                where server.Responsible == responsibleName
                select new DetailsSites
                {
                    SiteId = site.SiteId,
                    PhysicalPath = site.PhysicalPath,
                    SiteName = site.Name,
                    ServerName = server.ServerName,
                    DnsName = server.HostName,
                    State = site.State,
                    AppType = site.AppType,
                    Domains = server.HostName
                };
            return query.Distinct(DetailsSiteComparer.Instance);
        }

        public IEnumerable<RespSearchResult> GetResponsibles()
        {
            return _context.Servers.ToList()
                .Select(s => new RespSearchResult
                {
                    ResponsibleName = s.Responsible,
                    CountOfServers = GetResponsibleServerCount(s.Responsible),
                    CountOfSites = GetResponsibleSiteCount(s.Responsible)
                });
        }

        private string GetServerCompany(int id)
        {
            return _context.Companies.FirstOrDefault(c => c.CompanyId == id)?.Name;
        }

        public byte[] DownloadResponsibles()
        {
            return DownloadResponsibles(null);
        }

        public byte[] DownloadResponsibles(object term)
        {
            IEnumerable<RespSearchResult> results = null;
            if (term == null)
                results = GetResponsibles();
            else
                results = SearchResponsibles(term);

            return ExcelOperations.ExportToExcel(results);
        }
    }
}