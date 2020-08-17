namespace Application.V1.Users.Commands.Register
{
    public class RegisterCommandResponseDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
