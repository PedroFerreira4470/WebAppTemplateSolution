namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserId();
        string GetEmail();
        string GetUsername();
        string GetUserGlobalization();
    }
}
