using Application.Users;
using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <response code="200">Return Token</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody]LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync([FromBody] RegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpGet]
        //public async Task<ActionResult<UserDto>> CurrentUser()
        //{
        //    return await Mediator.Send(new CurrentUser.Query());
        //}

    }
}
