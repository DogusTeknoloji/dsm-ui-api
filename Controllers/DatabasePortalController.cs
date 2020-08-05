using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        public IActionResult Databases(int page)
        {
            return Ok(_databasePortalService.GetEnvanterAllDBEnvanter(page));
        }

        [HttpGet("details")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult Details(int id)
        {
            //1 SQL
            //2 Oracle
            //3 PostgreSQL
            return Ok(_databasePortalService.Details(id));
        }

        [HttpGet("search")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public ActionResult Search(object term)
        {
            return Ok(_databasePortalService.Search(term));
        }
    }
}
