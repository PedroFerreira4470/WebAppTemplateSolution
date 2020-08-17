using Application.Common.Contracts.V1.ResponseType;
using MediatR;

namespace Application.V1.Users.Commands.Login
{
    public class LoginQuery : IRequest<Response<LoginResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
