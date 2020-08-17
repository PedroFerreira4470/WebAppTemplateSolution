using Application.V1.Values.Commands.CreateValue;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace WebAPI.SwaggerDocExample.V1.Values.Request
{
    public class CreateValueCommandExample : IExamplesProvider<CreateValueCommand>
    {
        public CreateValueCommand GetExamples()
        {
            return new CreateValueCommand
            {
                ValueNumber = new Random().Next(0, 100)
            };
        }
    }
}
