namespace Application.V1.Users.Commands.Login
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
