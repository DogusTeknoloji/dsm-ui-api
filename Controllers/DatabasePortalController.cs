using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DSM.UI.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatabasePortalController : ControllerBase
    {
        private readonly IDatabasePortalService _databasePortalService;
        public DatabasePortalController(IDatabasePortalService databasePortalService, IOptions<AppSettings> appSettings)
        {
            _databasePortalService = databasePortalService;
        }

        [HttpGet("databases")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> Databases(int page)
        {
            return Ok(await _databasePortalService.GetEnvanterAllDBEnvanter(page));
        }

        [HttpGet("details")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> Details(int id)
        {
            //1 SQL
            //2 Oracle
            //3 PostgreSQL
            return Ok(await _databasePortalService.Details(id));
        }

        [HttpGet("search")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public ActionResult Search(string term)
        {
            return Ok(_databasePortalService.Search(term));
        }
    }
}
