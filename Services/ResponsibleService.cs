using System.Collections.Generic;
using System.Linq;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CompanyComparer;
using DSM.UI.Api.Models.Company;
using DSM.UI.Api.Models.Responsible;
using DSM.UI.Api.Models.Server;
using Microsoft.EntityFrameworkCore;
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
           return _context.Servers.Where(server => server.Responsible == responsibleName)
               .Distinct()
               .ToList()
               .Select(server =>
               {
                       return new DetailsServers
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
                           Owner = GetServerCompany((int)server.CompanyId!)
                       };
               }).Distinct();
        }

        public IEnumerable<DetailsSites> GetDetailsSites(string responsibleName)
        {
            
            return _context.Servers
                .Join(_context.Sites,
                    server => server.ServerName.ToUpper(),
                    site => site.MachineName.ToUpper(),
                    (server, site) => new { server, site })
                .Where(x => x.server.Responsible == responsibleName)
                .Select(x => new DetailsSites
                {
                    SiteId = x.site.SiteId,
                    PhysicalPath = x.site.PhysicalPath,
                    SiteName = x.site.Name,
                    ServerName = x.server.ServerName,
                    DnsName = x.server.HostName,
                    State = x.site.State,
                    AppType = x.site.AppType,
                    Domains = x.server.HostName
                }).Distinct().ToList();
        }

        public IEnumerable<RespSearchResult> GetResponsibles()
        {
            return _context.Servers.Select(x => x.Responsible)
                .Distinct()
                .ToList()
                .Select(responsible =>
                    new RespSearchResult
                    {
                        ResponsibleName = responsible,
                        CountOfServers = GetResponsibleServerCount(responsible),
                        CountOfSites = GetResponsibleSiteCount(responsible)
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