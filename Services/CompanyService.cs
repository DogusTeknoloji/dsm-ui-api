using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CompanyComparer;
using DSM.UI.Api.Models.Company;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
    }
    public class CompanyService : ICompanyService
    {
        private DSMStorageDataContext _context;
        public CompanyService(DSMStorageDataContext context)
        {
            _context = context;
        }
        public IEnumerable<DetailsServers> GetDetailsServers(int id)
        {
            Company result = this._context.Companies.Find(id);
            var query = from server in result.Servers
                        join appServer in this._context.ApplicationServers
                        on server.MachineName.ToUpper() equals appServer.ServerName.ToUpper() into leftjoinq
                        from jObj in leftjoinq.DefaultIfEmpty()
                        select new DetailsServers
                        {
                            ServerId = server.Id,
                            ApplicationType = jObj?.ApplicationName ?? "Unknown",
                            ServerName = server.MachineName,
                            Contact = server.Responsible,
                            Environments = jObj?.Environment ?? "Unknown",
                            FullName = server.DnsName,
                            IpAddress = server.IpAddress,
                            LastBackupDate = server.LastBackup.ToString(),
                            OperatingSystem = server.OperatingSystem,
                            Owner = jObj?.Owner ?? "Unknown"
                        };
            return query.Distinct(DetailsServerComparer.Instance);
        }

        public IEnumerable<DetailsSites> GetDetailsSites(int id)
        {
            Company result = this._context.Companies.Find(id);
            var query = from server in result.Servers
                        join site in this._context.Sites
                        on server.MachineName.ToUpper() equals site.MachineName.ToUpper()
                        select new DetailsSites
                        {
                            SiteId = site.SiteId,
                            PhysicalPath = site.PhysicalPath,
                            SiteName = site.Name,
                            ServerName = server.MachineName,
                            DnsName = server.DnsName,
                            State = site.State,
                            AppType = site.AppType,
                            Domains = server.MachineName
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
                var query = records.Take(pageItemCount).Select(x => new SearchResult()
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
            else
            {
                var query = records.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
        }

        public IEnumerable<SearchResult> GetCompanies(int pagenumber)
        {
            int pageItemCount = 100;
            if (pagenumber < 2)
            {
                var query = this._context.Companies.Take(pageItemCount).Select(x => new SearchResult()
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
            else
            {
                var query = this._context.Companies.Skip((pagenumber - 1) * pageItemCount).Take(pageItemCount).Select(x => new SearchResult()
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name
                });
                return query;
            }
        }

        public IEnumerable<SearchResult> SearchCompanies(object term)
        {
            object queryItem = term;
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
    }
}
