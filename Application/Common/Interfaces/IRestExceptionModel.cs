using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IRestExceptionModel
    {
        Dictionary<string, string[]> Errors { get; }
    }
}
