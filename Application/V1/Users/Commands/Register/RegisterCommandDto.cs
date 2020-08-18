namespace Application.V1.Users.Commands.Register
{
    public class RegisterCommandDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
