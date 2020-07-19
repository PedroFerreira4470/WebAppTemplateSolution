
using Application.Common.HelperExtensions;
using FluentAssertions;
using NUnit.Framework;


namespace Application.UnitTest.HelperExtensions
{
    using static ObjectExtensions;
    public class ObjectExtensionsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, new int[] { 1, 2, 3 })]
        public void IsIn_ShouldGetTrue_WhenIsInsideArray(int element, params int[] array)
        {
            //act
            var containsElement = element.IsIn(array);
            //Assert
            containsElement.Should().BeTrue();
        }

        [Test]
        [TestCase(0, new int[] { 1, 2, 3 })]
        public void IsIn_ShouldGetFalse_WhenIsNotInsideArray(int element, params int[] array)
        {
            //act
            var containsElement = element.IsIn(array);
            //Assert
            containsElement.Should().BeFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsBetweenInclusive_ShouldGetTrue_WhenNumberIsBetweenTwoNumbersInclusive(int element)
        {
            //act
            var containsElement = element.IsBetween(-1, 10, true);
            //Assert
            containsElement.Should().BeTrue();
        }

        [Test]
        [TestCase(11)]
        [TestCase(-2)]
        public void IsBetweenInclusive_ShouldGetFalse_WhenOutSideTwoNumbersNotInclusive(int element)
        {
            //act
            var containsElement = element.IsBetween(-1, 10, true);
            //Assert
            containsElement.Should().BeFalse();
        }

        [Test]
        [TestCase(6)]
        [TestCase(9)]
        public void IsBetweenExclusive_ShouldGetTrue_WhenNumberIsBetweenTwoNumbersExclusive(int element)
        {
            //act
            var containsElement = element.IsBetween(5, 10, false);
            //Assert
            containsElement.Should().BeTrue();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        public void IsBetweenExclusive_ShouldGetFalse_WhenOutSideTwoNumbersNotExclusive(int element)
        {
            //act
            var containsElement = element.IsBetween(-1, 10, false);
            //Assert
            containsElement.Should().BeFalse();
        }
    }
}
