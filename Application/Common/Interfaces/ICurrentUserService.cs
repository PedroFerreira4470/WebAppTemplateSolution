namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetEmail();
        string GetUsername();
        string GetUserGlobalization();
    }
}
