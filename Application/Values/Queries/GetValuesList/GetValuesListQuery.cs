using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Values.Queries.GetValuesList
{
   public class GetValuesListQuery : IRequest<List<ValuesListDto>>
   {
        //No params
   }
}
