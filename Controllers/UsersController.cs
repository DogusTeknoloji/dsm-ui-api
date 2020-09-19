using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.RemoteDesktop;
using DSM.UI.Api.Models.User;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            this._userService = userService;
            this._appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel userParam)
        {
            GetUserModel user = null;
            string message = "";
            DomainUserHolder holder = LDAPAuthService.ValidateUser(userParam.Username, userParam.Password, this._userService, out message);

            DomainUserInfo domainUser = holder?.DomainUser;

            if (domainUser != null)
            {
                // if user is not registered, register first
                if (holder.User == null)
                {
                    RegisterModel model = MapHelper.Map<RegisterModel, DomainUserInfo>(holder.DomainUser);
                    _ = this.Register(model);

                    user = MapHelper.Map<GetUserModel, User>(_userService.GetByUserName(userParam.Username));
                    if (user == null) return StatusCode(500, new { message = "LDAP Register failed." });
                }
                else
                {
                    user = holder.User;
                }
            }
            else if (message.ToLower() != "success")
            {
                return BadRequest(new { message = message });
            }
            else
            {
                if (string.IsNullOrEmpty(userParam.Username) || string.IsNullOrEmpty(userParam.Password))
                {
                    return BadRequest(new { message = "Password is Required" });
                }

                User xUser = _userService.Authenticate(userParam.Username, userParam.Password);
                user = MapHelper.Map<GetUserModel, User>(xUser);

                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else if (!user.Enabled)
                {
                    return BadRequest(new { message = "This user is disabled." });
                }
            }

            //Continue to login operation...
            string tokenString = AuthenticationHelper.GetToken(user, _appSettings.Secret);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.FullName,
                user.AccountCreateDate,
                user.Company,
                user.DateOfHire,
                user.Department,
                user.EmployeeId,
                user.LastLogonTime,
                user.Location,
                user.LogonCount,
                user.MailNickName,
                user.MobilePhone,
                user.OfficeName,
                user.PasswordLastSet,
                user.SamAccountName,
                user.Title,
                Role = user.Role?.Name,
                Token = tokenString,
                ProfilePhoto = user.ProfileImage,

                IsAdUser = domainUser != null
            });
        }

        [AllowAnonymous]
        [HttpPost("authenticateldap")]
        public IActionResult AuthenticateLDAP()
        {
            GetUserModel user = null;
            DomainUserInfo domainUser = LDAPAuthService.GetCurrentUser();
            if (domainUser == null) return StatusCode(401, new { message = "LDAP Auth failed." });
            user = MapHelper.Map<GetUserModel, User>(this._userService.GetByUserName(domainUser.Username));
            if (user == null)
            {
                RegisterModel model = MapHelper.Map<RegisterModel, DomainUserInfo>(domainUser);
                _ = this.Register(model);

                user = MapHelper.Map<GetUserModel, User>(this._userService.GetByUserName(domainUser.Username));
                if (user == null) return StatusCode(500, new { message = "LDAP Register failed." });
            }
            string tokenString = AuthenticationHelper.GetToken(user, this._appSettings.Secret);

            return Ok(new
            {
                user.Id,
                user.Username,
                domainUser.FullName,
                Role = user.Role?.Name,
                Token = tokenString,
                ProfilePhoto = domainUser.ProfileImage,
                IsAdUser = domainUser != null
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var user = MapHelper.Map<User, RegisterModel>(model);
            user.Id = 0;
            try
            {
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Administrator, CIFANG")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = MapHelper.Map<UserModel, User>(users);
            return Ok(model);
        }

        [HttpGet("{username}")]
        [Authorize(Roles = "Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetByUsername(string username)
        {
            var user = _userService.GetByUserName(username);
            var model = MapHelper.Map<UserModel, User>(user);
            if (user == null)
            {
                return NotFound();
            }

            var currentUsername = User.Identity.Name;
            if (username != currentUsername && !User.IsInRole(model.Role))
            {
                return Forbid();
            }

            return Ok(model);
        }

        [HttpPost("update/{username}")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public IActionResult Update(string username, [FromBody] UpdateModel model)
        {
            var user = MapHelper.Map<User, UpdateModel>(model);
            user.Username = username;

            try
            {
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
