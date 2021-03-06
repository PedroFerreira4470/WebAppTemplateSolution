﻿using Application.Common.CustomExceptions;
using Application.V1.Values.Commands.CreateValue;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.V1.Values.Commands.CreateValue
{
    using static Testing;
    public class CreateValueTest : TestBase
    {
        [Test]
        public async Task ShouldRequiredNumberAsync()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateValueCommand
            {
                ValueNumber = -123
            };

            FluentActions
                .Invoking(() => SendAsync(command))
                .Should().Throw<RestException>();
        }



    }
}
