﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task SetUpAsync() {
            await ResetStateAsync();
        }

        [TearDown]
        public async Task SetUp1Async()
        {
            await ResetStateAsync();
        }
    }
}