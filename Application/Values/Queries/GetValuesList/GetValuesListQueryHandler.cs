using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Helpers;

namespace Application.Values.Queries.GetValuesList
{
    public class GetValuesListQueryHandler : IRequestHandler<GetValuesListQuery, List<ValuesListDto>>
    {
        private readonly ITemplateDbContext _context;
        private readonly IMapper _mapper;

        public GetValuesListQueryHandler(ITemplateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ValuesListDto>> Handle(GetValuesListQuery request, CancellationToken cancellationToken)
        {

            var result = await _context.Values.Where(p => p.ValueNumber < 10)
                .ProjectTo<ValuesListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            //Using Dapper(other ORM, instead of EF to get Data from DB)
            //Note: Dapper is faster compared to EF for small-medium queries
            //var result1 = await _context.DbConnection.QueryAsync<Value>("select * from Values");
            //.QueryAsync<Value,Dto,return>("SELECT  * FROM [dbo].[Values]");

            if (result is null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Value = $"{nameof(Value)} not found" });
            }

            return result;
        }
    }
}
