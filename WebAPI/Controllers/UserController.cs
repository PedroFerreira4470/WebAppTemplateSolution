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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }

        //[HttpGet]
        //public async Task<ActionResult<UserDto>> CurrentUser()
        //{
        //    return await Mediator.Send(new CurrentUser.Query());
        //}

    }
}
