using Application.Common.MethodExtensions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.HelperExtensions
{
    using static StringExtensions;
    public class StringExtensionsTests
    {
        //IsValidEmailFormat
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("pedrodiogo@hotmail.com")]
        [TestCase("pedro123@gmail.com")]
        [TestCase("123@123.pt")]
        public void IsValidEmailFormat_ShouldBeTrue_WhenIsInEmailFormat(string email)
        {
            //act
            var result = email.IsValidEmailFormat();
            //Assert
            result.Should().BeTrue();
        }

        [Test]
        [TestCase("email.com")]
        [TestCase("123@gmail")]
        [TestCase("pedro..d@hotmail.com")]
        [TestCase("pedro.d@@hotmail.com")]
        [TestCase("123")]
        [TestCase(".@.")]
        [TestCase("")]
        [TestCase(null)]
        public void IsValidEmailFormat_ShouldBeFalse_WhenIsInvalidEmailFormat(string email)
        {
            //act
            var result = email.IsValidEmailFormat();
            //Assert
            result.Should().BeFalse();
        }
    }
}
