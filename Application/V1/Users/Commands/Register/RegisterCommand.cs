using Application.Common.Contracts.V1.ResponseType;
using MediatR;

namespace Application.V1.Users.Commands.Register
{
    public class RegisterCommand : IRequest<Response<RegisterCommandDto>>
    {

        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
