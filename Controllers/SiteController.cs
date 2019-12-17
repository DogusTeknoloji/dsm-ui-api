using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Site;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private ISiteService _siteService;
        private readonly AppSettings _appSettings;

        public SiteController(ISiteService siteService, IOptions<AppSettings> appSettings)
        {
            this._siteService = siteService;
            this._appSettings = appSettings.Value;
        }

        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        [HttpGet("Search/{term}")]
        public IActionResult Search(string term)
        {
            var searchResults = _siteService.SearchSites(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });

            var model = MapHelper.Map<SearchResult, Core.Models.Site>(searchResults);
            return Ok(model);
        }

        [HttpGet("header/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsHeader(int id)

        {
            var headerInfo = _siteService.GetHeader(id);
            if (headerInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(headerInfo);
        }
        [HttpGet("details/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsGeneral(int id)
        {
            var generalInfo = _siteService.GetDetailsGeneral(id);
            if (generalInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(generalInfo);
        }

        [HttpGet("bindings/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsBindings(int id)
        {
            var bindingInfo = _siteService.GetDetailsBindings(id);
            if (bindingInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(bindingInfo);
        }

        [HttpGet("packages/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsPackage(int id)
        {
            var packageInfo = _siteService.GetDetailsPackages(id);
            if (packageInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(packageInfo);
        }

        [HttpGet("endpoints/{id}")]
        [Authorize(Roles = "Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsEndpoints(int id)
        {
            var endpointInfo = _siteService.GetDetailsEndpoint(id);
            if (endpointInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(endpointInfo);
        }

        [HttpGet("connectionstrings/{id}")]
        [Authorize(Roles = "Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsConnectionString(int id)
        {
            var connectionstringInfo = _siteService.GetDetailsConnectionStrings(id);
            if (connectionstringInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(connectionstringInfo);
        }
        [HttpGet("{pageNumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSites(int pageNumber)
        {
            var siteInfo = _siteService.GetSites(pageNumber);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            var model = MapHelper.Map<SearchResult, Core.Models.Site>(siteInfo);

            return Ok(model);
        }
        [HttpGet("letters")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetLetters()
        {
            var letters = this._siteService.GetLetters();
            if (letters == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(letters);
        }

        [HttpGet("letters/{letter}&{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSitesByLetter(string letter, int pagenumber)
        {
            var siteInfo = this._siteService.GetSitesByLetter(letter, pagenumber);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(siteInfo);
        }
    }
}
