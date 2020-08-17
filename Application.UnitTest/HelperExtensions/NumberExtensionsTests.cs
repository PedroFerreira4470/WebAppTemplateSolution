using Application.Common.MethodExtensions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Application.UnitTests.HelperExtensions
{
    using static NumberExtensions;
    public class NumberExtensionsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(5, 1)]
        [TestCase(0, -1)]
        [TestCase(100, 2)]
        [TestCase(-5, -10)]
        [TestCase(1.2, 1.1)]
        [TestCase(0, -1.2)]
        [TestCase(1.2222, 1.2221)]
        public void IsBiggerThan_ShouldBeTrue_WhenNumb1IsBiggerThanNumb2<T>(T n1, T n2)
            where T : struct, IConvertible
        {
            //act
            var result = n1.IsBiggerThan(n2);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        [TestCase(1, 5)]
        [TestCase(-1, 0)]
        [TestCase(2, 100)]
        [TestCase(-10, -5)]
        [TestCase(1.1, 1.2)]
        [TestCase(-1.2, 0)]
        [TestCase(1.2221, 1.2222)]
        public void IsBiggerThan_ShouldBeFalse_WhenNumb1IsSmallerThanNumb2<T>(T n1, T n2)
          where T : struct, IConvertible
        {
            //act
            var result = n1.IsBiggerThan(n2);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(1.1, 1.1)]
        [TestCase(-1.1, -1.1)]
        public void IsBiggerThan_ShouldBeFalse_WhenBothNumbersAreTheSame<T>(T n1, T n2)
            where T : struct, IConvertible
        {
            //act
            var result = n1.IsBiggerThan(n2);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        [TestCase(2)]
        [TestCase(-2)]
        [TestCase(50)]
        public void IsValueOdd_ShouldBeFalse_WhenNumberIsEven<T>(T n1)
            where T : struct, IConvertible
        {
            //act
            var result = n1.IsValueOdd();
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        [TestCase(3)]
        [TestCase(-1)]
        [TestCase(1.1)]
        public void IsValueOdd_ShouldBeTrue_WhenNumberIsOdd<T>(T n1)
            where T : struct, IConvertible
        {
            //act
            var result = n1.IsValueOdd();

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        [TestCase(3)]
        [TestCase(-1)]
        [TestCase(1.1)]
        public void IsValueEven_ShouldBeFalse_WhenNumberIsOdd<T>(T n1)
           where T : struct, IConvertible
        {
            //act
            var result = n1.IsValueEven();

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(-2)]
        [TestCase(50)]
        public void IsValueEven_ShouldBeTrue_WhenNumberIsEven<T>(T n1)
            where T : struct, IConvertible
        {
            //act
            var result = n1.IsValueEven();

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsValueEven_ShouldThowArgumentException_WhenTypeDate()
        {
            //arrange
            var date = DateTime.Now;

            //act
            Action act = () => date.IsValueEven();

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("Date Format is not Valid");
        }

        [Test]
        public void IsValueOdd_ShouldThowArgumentException_WhenTypeDate()
        {
            //arrange
            var date = DateTime.Now;
            //act
            Action act = () => date.IsValueEven();

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("Date Format is not Valid");
        }

    }
}
