using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application._CustomExceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Values.Queries.GetValuesList
{
    public class GetValuesListQueryHandler : IRequestHandler<GetValuesListQuery, List<ValuesListDto>>
    {
        private readonly TemplateDbContext _context;
        private readonly IMapper _mapper;

        public GetValuesListQueryHandler(TemplateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ValuesListDto>> Handle(GetValuesListQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Values
                .ProjectTo<ValuesListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (result is null)
                throw new RestException(HttpStatusCode.NotFound, new { Value = $"{nameof(Value)} not found" });

            return result;
        }
    }
}
