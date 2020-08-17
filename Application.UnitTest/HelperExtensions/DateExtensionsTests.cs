using Application.Common.MethodExtensions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Application.UnitTests.HelperExtensions
{
    using static DateExtensions;
    public class DateExtensionsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetDatesUntil_ShouldGetArray_WhenCorrectDates()
        {
            //Arrange
            var date = DateTime.Now;
            var anotherDay = date.AddDays(5);

            //act
            var arrayOfDates = date.GetDatesUntil(anotherDay);
            //Assert
            arrayOfDates.Should().NotBeNull();
            arrayOfDates.Should().NotBeEmpty();
            arrayOfDates.Should().HaveCount(6);
            arrayOfDates.Should().StartWith(date);
            arrayOfDates.Should().EndWith(anotherDay);
        }

        [Test]
        public void GetDatesUntil_ShouldBeEmptyArray_WhenTheSecondDateComesBeforeTheFirstDate()
        {
            //Arrange
            var date = DateTime.Now;
            var anotherDay = date.AddDays(-2);

            //act
            var arrayOfDates = date.GetDatesUntil(anotherDay);
            //Assert
            arrayOfDates.Should().BeEmpty();
        }

        [Test]
        public void GetDatesUntil_ShouldBeOneDate_WhenTheSameDay()
        {
            //Arrange
            var date = DateTime.Now;
            //act
            var arrayOfDates = date.GetDatesUntil(date);
            //Assert
            arrayOfDates.Should().NotBeNull();
            arrayOfDates.Should().NotBeEmpty();
            arrayOfDates.Should().HaveCount(1);
            arrayOfDates.Should().StartWith(date);
            arrayOfDates.Should().EndWith(date);
        }

        //[Test]
        //public void IsBetweenInclusive_ShouldBeTrueIfSameDay()
        //{
        //    //Arrange
        //    var date = DateTime.Now;
        //    //act
        //    var result = date.IsBetween(date,date,true);
        //    //Assert
        //    result.Should().BeTrue();

        //}

        //[Test]
        //public void IsBetweenInclusive_ShouldBeTrue()
        //{
        //    //Arrange
        //    var date = DateTime.Now;
        //    //act
        //    var result = date.IsBetween(date.AddDays(-3), date.AddDays(2), true);
        //    //Assert
        //    result.Should().BeTrue();
        //}

        //[Test]
        //public void IsBetweenInclusive_ShouldBeFalse()
        //{
        //    //Arrange
        //    var date = DateTime.Now;
        //    //act
        //    var result = date.IsBetween(date.AddDays(3), date.AddDays(7), true);
        //    //Assert
        //    result.Should().BeFalse();
        //}

        //[Test]
        //public void IsBetweenExclusive_ShouldBeFalseIfSameDay()
        //{
        //    //Arrange
        //    var date = DateTime.Now;
        //    //act
        //    var result = date.IsBetween(date, date, false);
        //    //Assert
        //    result.Should().BeFalse();
        //}

        //[Test]
        //public void IsBetweenExclusive_ShouldBeTrue()
        //{
        //    //Arrange
        //    var date = DateTime.Now;
        //    //act
        //    var result = date.IsBetween(date.AddDays(-1), date.AddDays(1), false);
        //    //Assert
        //    result.Should().BeTrue();
        //}
    }
}
