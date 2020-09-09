using Application.Common.Contracts.V1.QueryTypes;
using Application.Common.Contracts.V1.ResponseType;
using AutoMapper.Configuration.Annotations;
using MediatR;
using System.Runtime.Serialization;

namespace Application.V1.Values.Queries.GetValuesList
{

    public class GetValuesListQuery : PaginationQuery, IRequest<<PagedResponse<GetValuesListDto>>>
    {
        private const int DEFAULT_PAGE_SIZE = 2;
        private const int DEFAULT_PAGE_NUMBER = 1;
        public int UserId { get; set; }
        public GetValuesListQuery()
        {
            PageNumber = DEFAULT_PAGE_NUMBER;
            PageSize = DEFAULT_PAGE_SIZE;
        }
    }

}
