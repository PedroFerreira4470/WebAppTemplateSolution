using System;

namespace Application.Common.Interfaces
{
    public interface IUriService
    {
        string GetAbsoluteUrl();

        string GetAbsolutePath();
        Uri GetAbsoluteUri();


    }
}
