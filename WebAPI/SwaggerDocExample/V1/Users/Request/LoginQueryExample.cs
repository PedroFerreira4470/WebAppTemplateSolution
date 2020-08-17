using Application.V1.Users.Commands.Login;
using Swashbuckle.AspNetCore.Filters;

namespace WebAPI.SwaggerDocExample.V1.Users.Request
{
    public class LoginQueryExample : IExamplesProvider<LoginQuery>
    {
        public LoginQuery GetExamples()
        {
            return new LoginQuery
            {
                Email = "p@p.com",
                Password = "Passw0rd!"

            };
        }
    }
}
