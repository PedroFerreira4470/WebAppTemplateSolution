using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Values.Queries.GetValuesList
{
    public class GetValuesListRequestValidator : AbstractValidator<GetValuesListQuery>
    {
        //No params = no validation
    }
}
