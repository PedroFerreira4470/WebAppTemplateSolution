using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Contracts.V1.QueryTypes
{
    public class PaginationQuery
    {

        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }
    }
}
