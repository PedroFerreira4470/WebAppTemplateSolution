using Application.Common.Contracts.V1.ResponseType;
using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static Application.Common.Helpers.V1.PaginationHelper;

namespace Application.V1.Values.Queries.GetValuesList
{
    public class GetValuesListQueryHandler : IRequestHandler<GetValuesListQuery, PagedResponse<GetValuesListDto>>
    {
        private readonly ITemplateDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GetValuesListQueryHandler(ITemplateDbContext context, IMapper mapper, IUriService uriService)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriService;
        }

        public async Task<PagedResponse<GetValuesListDto>> Handle(GetValuesListQuery request, CancellationToken cancellationToken)
        {
            var skip = ((request.PageNumber - 1) * request.PageSize);
            var take = request.PageSize;

            var result = await _context.Values
                .ProjectTo<GetValuesListDto>(_mapper.ConfigurationProvider)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);


            if (result is null)
            {
                throw new RestException(HttpStatusCode.NotFound, nameof(Value), $"{nameof(Value)} not found");
            }

            var totalRecords = await _context.Values.CountAsync(cancellationToken);

            return CreatePagedResponse(result, request.PageSize, request.PageNumber, totalRecords, _uriService);

        }
    }
}



//Using Dapper(other ORM, instead of EF to get Data from DB)
//Note: Dapper is faster compared to EF for small-medium queries
//var result1 = await _context.DbConnection.QueryAsync<Value>("select * from Values");
//.QueryAsync<Value,Dto,return>("SELECT  * FROM [dbo].[Values]");
