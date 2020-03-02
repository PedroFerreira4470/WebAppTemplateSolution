namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        string GetCurrentEmail();
        string GetCurrentUserName();
    }
}
