using System;
using Application.Values.Commands.CreateValue;
using Swashbuckle.AspNetCore.Filters;

namespace WebAPI.SwaggerFilters.Values.Commands
{
    public class CreateValueCommandFilter : IExamplesProvider<CreateValueCommand>
    {
        public CreateValueCommand GetExamples()
        {
            return new CreateValueCommand
            {
                ValueNumber = new Random().Next(0,100)
            };
        }
    }
}
