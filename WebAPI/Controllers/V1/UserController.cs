using Application.Common.Contracts.V1.ResponseType;
using Application.V1.Users.Commands.Login;
using Application.V1.Users.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class UserController : BaseV1Controller
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <response code="200">Return User dto with Token</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Response<LoginDto>>> LoginAsync([FromBody] LoginQuery query)
            => await Mediator.Send(query);

        /// <summary>
        /// Register user in the system
        /// </summary>
        /// <response code="200">Return Token</response>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Response<RegisterCommandDto>>> RegisterAsync([FromBody] RegisterCommand command)
            => await Mediator.Send(command);

        //[HttpGet]
        //public async Task<ActionResult<UserDto>> CurrentUser()
        //{
        //    return await Mediator.Send(new CurrentUser.Query());
        //}

    }
}
