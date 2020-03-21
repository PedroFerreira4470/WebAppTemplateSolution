namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetCurrentEmail();
        string GetCurrentUserName();
    }
}
