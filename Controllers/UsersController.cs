using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.User;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel userParam)
        {
            // Check username is exist?
            User user = _userService.GetByUserName(userParam.Username);

            int adDomainId = -1;
            //if format is valid for LDAP domains.
            if (Regex.IsMatch(userParam.Username, LDAPAuthService.MAILREGEXPATTERN))
            {
                // if exists, use user's previously saved domain info otherwise get all domains to find valid domain.
                List<Domain> ldapDomains = user == null ? _userService.GetDomainList() : new List<Domain> { user.Domain };
                //Split by @ and take the second one In Example: username@contoso-domain.com take contoso-domain.com
                string domainName = userParam.Username.Split('@')[1];
                //Replace not allowed '-' char to empty literal; So contoso-domain.com -> contosodomain.com
                domainName = domainName.Replace("-", "");
                // Split by '.' and take first part of string; So contosodomain.com -> contosodomain
                domainName = domainName.Split('.')[0];
                // Create new minified list by which item contains contosodomain in domainlist
                ldapDomains = ldapDomains.Where(x => x.DomainName.Contains(domainName)).ToList();
                // Validate user in domains in the list, if found return domain id else return -1
                adDomainId = LDAPAuthService.AuthenticateActiveDirectory(ldapDomains, userParam.Username, userParam.Password);
            }

            if (adDomainId != -1)
            {
                int retry = 0;
                user = _userService.GetByUserName(userParam.Username);
                // if user is not registered, register first
                while (user == null && retry < 5)
                {
                    RegisterModel model = new RegisterModel
                    {
                        Username = userParam.Username,
                        Password = userParam.Password,
                        FullName = userParam.Username.Split('@')[0],
                        DomainId = adDomainId
                    };
                    UsersController usersController = this;
                    var result = usersController.Register(model);

                    user = _userService.GetByUserName(userParam.Username);
                    retry++;
                }
            }
            else
            {
                user = _userService.Authenticate(userParam.Username, userParam.Password);

                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
            }

            //Continue to login operation...

            Dictionary<string, object> claims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, user.FullName },
                { ClaimTypes.Actor, user.Username },
                { ClaimTypes.Role, user.Role.Name }
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = claims,
                Issuer = "Doğuş Teknoloji",
                Audience = "DT Users",
                IssuedAt = DateTime.UtcNow
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.FullName,
                Role = user.Role?.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel model)
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
        public IActionResult Update(string username, [FromBody]UpdateModel model)
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
