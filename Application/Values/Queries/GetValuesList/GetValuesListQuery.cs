using MediatR;
using System.Collections.Generic;

namespace Application.Values.Queries.GetValuesList
{
    public class GetValuesListQuery : IRequest<List<ValuesListDto>>
    {
        //No params
    }
}
