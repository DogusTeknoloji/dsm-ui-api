using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CompanyComparer;
using DSM.UI.Api.Models.Company;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;

namespace DSM.UI.Api.Services
{
    public interface ICompanyService
    {
        IEnumerable<SearchResult> SearchCompanies(object term);
        DetailsHeader GetHeader(int id);
        IEnumerable<DetailsSites> GetDetailsSites(int id);
        IEnumerable<DetailsServers> GetDetailsServers(int id);
        IEnumerable<string> GetLetters();
        IEnumerable<SearchResult> GetCompanyByLetter(string letter, int pagenumber);
        IEnumerable<SearchResult> GetCompanies(int pagenumber);
        IEnumerable<SearchResult> GetCompaniesIfAnyServerExists(int pagenumber);
        int GetCompanyServerCount(int companyId);
        int GetCompanySiteCount(int companyId);
        byte[] DownloadCompanies(object term);
        byte[] DownloadCompanies();
    }
    public class CompanyService : ICompanyService
    {
        private readonly DSMStorageDataContext _context;
        public CompanyService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<DetailsServers> GetDetailsServers(int id)
        {
            Company result = this._context.Companies.Find(id);
            var query = from server in result.Servers
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
                            Owner = result.Name
                        };
            return query.Distinct(DetailsServerComparer.Instance);
        }

        public IEnumerable<DetailsSites> GetDetailsSites(int id)
        {
            Company result = this._context.Companies.Find(id);
            var query = from server in result.Servers
                        join site in this._context.Sites
                        on server.ServerName.ToUpper() equals site.MachineName.ToUpper()
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

        public DetailsHeader GetHeader(int id)
        {
            Company result = this._context.Companies.Find(id);
            DetailsHeader header = new DetailsHeader
            {
                CompanyName = result.Name
            };
            return header;
        }

        public IEnumerable<string> GetLetters()
        {
            var firstLetters = this._context.Companies.GroupBy(s => s.Name.Substring(0, 1)).Select(x => x.Key.ToUpper()).ToList();
            firstLetters = firstLetters.OrderBy(x => x).ToList();
            firstLetters.Add("Tümü");
            return firstLetters;
        }

        public IEnumerable<SearchResult> GetCompanyByLetter(string letter, int pagenumber)
        {
            int pageItemCount = 100;
            var records = this._context.Companies.Where(x => x.Name.StartsWith(letter));

            if (pagenumber < 2)
            {
                var query = records.Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
            else
            {
                var query = records.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
        }

        public int GetCompanySiteCount(int companyId)
        {
            return (from server in _context.Servers
                join site in _context.Sites
                    on server.ServerName.ToUpper() equals site.MachineName.ToUpper()
                where server.CompanyId == companyId
                select site).Count();
        }

        public int GetCompanyServerCount(int companyId)
        {
            return _context.Servers.Count(c => c.CompanyId == companyId);
        }
        
        public IEnumerable<SearchResult> GetCompanies(int pagenumber)
        {
            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = this._context.Companies.Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
            else
            {
                var query = this._context.Companies.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
        }
        
        public IEnumerable<SearchResult> GetCompaniesIfAnyServerExists(int pagenumber)
        {
            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = this._context.Companies.Where(x => x.Servers.Any()).Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
            else
            {
                var query = this._context.Companies.Where(x => x.Servers.Any()).Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
        }

        public IEnumerable<SearchResult> SearchCompanies(object term)
        {
            IEnumerable<PropertyInfo> stringProperties = typeof(Company).GetProperties().Where(prop => prop.PropertyType == term.GetType());

            var query = from a in _context.Companies select a;
            query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());

            var results = query.ToList();

            return results.Select(x => new SearchResult
            {
                CompanyId = x.CompanyId,
                Name = x.Name
            });
        }
        public byte[] DownloadCompanies()
        {
            return this.DownloadCompanies(null);
        }
        public byte[] DownloadCompanies(object term)
        {

            IEnumerable<SearchResult> results = null;
            if (term == null)
            {
                var query = this._context.Companies;
                results = query.ToList().Select(x => new SearchResult { CompanyId = x.CompanyId, Name = x.Name });
            }
            else
            {
                IEnumerable<PropertyInfo> stringProperties = typeof(Company).GetProperties().Where(prop => prop.PropertyType == term.GetType());

                var query = from a in _context.Companies select a;
                query = EntityQueryable.WhereContains(query, fields: stringProperties, term.ToString());
                results = query.ToList().Select(x => new SearchResult { CompanyId = x.CompanyId, Name = x.Name });
            }

            return ExcelOperations.ExportToExcel(results);
        }
    }
}
