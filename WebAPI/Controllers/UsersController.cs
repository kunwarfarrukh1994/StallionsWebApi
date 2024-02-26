using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BAL.interfaces;
using WEBAPI.DAL.models;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Create")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost(Name = "AddUser")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Create")]
        public async Task<ActionResult<User>> AddUser(User newUser)
        {
            var addedUser = await _userService.AddUserAsync(newUser);
            return Ok(addedUser);
        }
    }
}
