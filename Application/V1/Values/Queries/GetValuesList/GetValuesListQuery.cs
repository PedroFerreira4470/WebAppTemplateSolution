using Application.Common.Contracts.V1.QueryTypes;
using Application.Common.Contracts.V1.ResponseType;
using MediatR;

namespace Application.V1.Values.Queries.GetValuesList
{

    public class GetValuesListQuery : IRequest<PagedResponse<ValuesListResponseDto>>
    {
        private const int MAX_PAGE_SIZE = 100;
        private const int DEFAULT_PAGE_SIZE = 2;
        private const int DEFAULT_PAGE_NUMBER = 1;

        public int PageNumber { get; }
        public int PageSize { get; }

        public GetValuesListQuery()
        {
            PageNumber = DEFAULT_PAGE_NUMBER;
            PageSize = DEFAULT_PAGE_SIZE;
        }
        public GetValuesListQuery(PaginationQuery query)
        {
            if (query.PageSize <= 0)
            {
                query.PageSize = DEFAULT_PAGE_SIZE;
            }

            if (query.PageNumber <= 0)
            {
                query.PageNumber = DEFAULT_PAGE_NUMBER;
            }
            PageNumber = query.PageNumber == 0 ? DEFAULT_PAGE_NUMBER : query.PageNumber;
            PageSize = query.PageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : query.PageSize;

        }
    }

}
