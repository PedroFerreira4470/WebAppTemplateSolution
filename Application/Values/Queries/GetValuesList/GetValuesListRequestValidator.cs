using FluentValidation;

namespace Application.Values.Queries.GetValuesList
{
    public class GetValuesListRequestValidator : AbstractValidator<GetValuesListQuery>
    {
        //No params = no validation
    }
}
