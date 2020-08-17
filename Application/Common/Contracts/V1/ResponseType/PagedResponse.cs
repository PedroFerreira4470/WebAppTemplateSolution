using System.Collections.Generic;

namespace Application.Common.Contracts.V1.ResponseType
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {

        }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string NextPage { get; set; }

        public string PreviousPage { get; set; }
    }
}
