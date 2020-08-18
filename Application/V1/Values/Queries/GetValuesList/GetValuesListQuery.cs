using Application.Common.Contracts.V1.QueryTypes;
using Application.Common.Contracts.V1.ResponseType;
using MediatR;

namespace Application.V1.Values.Queries.GetValuesList
{

    public class GetValuesListQuery : PaginationQuery, IRequest<PagedResponse<GetValuesListDto>>
    {
        private const int DEFAULT_PAGE_SIZE = 2;
        private const int DEFAULT_PAGE_NUMBER = 1;

        public GetValuesListQuery()
        {
            PageNumber = DEFAULT_PAGE_NUMBER;
            PageSize = DEFAULT_PAGE_SIZE;
        }
    }

}
