using BC.Server.Models.DB;
using BC.Server.Services;
using BC.Shared;
using BC.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BC.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IUserValidationService _userValidation;

        public UserController(DataContext context, ILogger<UserController> logger, IUserValidationService userValidation)
        {
            _context = context;
            _logger = logger;
            _userValidation = userValidation;
        }

        private async Task AuthenticateUser(UserProfile user)
        {
            var claim = new Claim(ClaimTypes.Name, user.EmailAddress);
            var identity = new ClaimsIdentity(new Claim[] { claim }, "ServerAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserProfileVM>> LogInUser([FromBody]LogInVM logIn)
        {
            UserProfile match = null;
            try
            {
                match = await _userValidation.LogInUser(logIn);
            } catch(Exception ex) { return new UnauthorizedObjectResult(ex.Message); }
            if (match != null)
                await AuthenticateUser(match);
            if (match == null)
                return new UnauthorizedResult();
            else return new OkObjectResult((UserProfileVM)match);
        }

        [HttpGet("current")]
        public async Task<ActionResult<UserProfileVM>> GetCurrentUser()
        {
            UserProfileVM user = null;
            if(User.Identity.IsAuthenticated)
            {
                user = _context.UserProfiles.FirstOrDefault(o => o.EmailAddress == User.FindFirstValue(ClaimTypes.Name));
            }
            return user == null ? new UserProfileVM() : user;
        }

        [HttpGet("logout")]
        public async Task<ActionResult> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return new OkResult();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserProfileVM>> PostRegister([FromBody]RegistrationVM vm)
        {
            UserProfile match = null;
            try
            {
                match = await _userValidation.RegisterUser(vm);
            }
            catch (Exception ex) { return new UnauthorizedObjectResult(ex.Message); }
            return new OkObjectResult((UserProfileVM)match);
        }
    }
}
