using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        private IServerService _serverService;
        private readonly AppSettings _appSettings;
        public ServerController(IServerService serverService, IOptions<AppSettings> appSettings)
        {
            this._serverService = serverService;
            this._appSettings = appSettings.Value;
        }
        [HttpGet("Search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult Search(string term)
        {
            var searchResults = this._serverService.SearchServers(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });

            return Ok(searchResults);
        }
        [HttpGet("header/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsHeader(int id)
        {
            var headerInfo = this._serverService.GetHeader(id);
            if (headerInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(headerInfo);
        }
        [HttpGet("details/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsGeneral(int id)
        {
            var generalInfo = this._serverService.GetDetailsGeneral(id);
            if (generalInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(generalInfo);
        }
        [HttpGet("{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServers(int pagenumber)
        {
            var serverInfo = this._serverService.GetServers(pagenumber);
            if (serverInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(serverInfo);
        }
        [HttpGet("letters")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetLetters()
        {
            var letters = this._serverService.GetLetters();
            if (letters == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(letters);
        }
        [HttpGet("letters/{letter}&{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServersByLetter(string letter, int pagenumber)
        {
            var serverInfo = this._serverService.GetServersByLetter(letter, pagenumber);
            if (serverInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(serverInfo);
        }
        [HttpGet("sites/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSites(int id)
        {
            var siteInfo = this._serverService.GetDetailsSites(id);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(siteInfo);
        }
    }
}
