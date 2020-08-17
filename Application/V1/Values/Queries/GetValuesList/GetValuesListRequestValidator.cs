using FluentValidation;

namespace Application.V1.Values.Queries.GetValuesList
{
    public class GetValuesListRequestValidator : AbstractValidator<GetValuesListQuery>
    {
        public GetValuesListRequestValidator()
        {
            //No params = no validation
        }

    }
}
