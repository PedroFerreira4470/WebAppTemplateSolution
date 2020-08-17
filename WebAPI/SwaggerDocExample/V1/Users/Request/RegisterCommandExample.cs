using Application.V1.Users.Commands.Register;
using Swashbuckle.AspNetCore.Filters;

namespace WebAPI.SwaggerDocExample.V1.Users.Request
{
    public class RegisterCommandExample : IExamplesProvider<RegisterCommand>
    {
        public RegisterCommand GetExamples()
        {
            return new RegisterCommand
            {
                UserName = "Hotshot123",
                Email = "pd@pd.c0m",
                DisplayName = "pd",
                Password = "Passw0rd!"

            };
        }
    }
}
