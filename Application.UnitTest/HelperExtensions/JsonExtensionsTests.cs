﻿using Application.Common.MethodExtensions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Application.UnitTests.HelperExtensions
{
    public class JsonExtensionsTests
    {
        internal enum EnumTeste
        {
            first,
            second
        }

        internal class Result
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
            public EnumTeste Enum { get; set; }
            public string NullValue { get; set; }
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializeToJson_ShouldBeConvertedToJson_WhenCorrectObjectFormat()
        {
            //Arrange
            var obj = new Result() { Name = "Teste", Time = DateTime.Now, Enum = EnumTeste.first, NullValue = null };
            //act
            var jsonObj = obj.SerializeToJson();
            //Assert
            jsonObj.Should().NotBeNullOrEmpty();
            jsonObj.Should().BeOfType(typeof(String));
        }

        [Test]
        public void SerializeToJson_ShouldHaveNullValues_WhenPropertiesAreNull()
        {
            //Arrange
            var obj = new Result() { Name = "Teste", Time = DateTime.Now, Enum = EnumTeste.first, NullValue = null };
            //act
            var jsonObj = obj.SerializeToJson();
            //Assert
            jsonObj.Should().NotBeNullOrEmpty();
            var field = "\"nullValue\": null";
            jsonObj.Should().Contain(field);
        }

        [Test]
        public void SerializeToJson_ShouldBeCamelCase_ToMeetSerializeCriteria()
        {
            //Arrange
            var obj = new Result() { Name = "Teste", Time = DateTime.Now, Enum = EnumTeste.first, NullValue = null };
            //act
            var jsonObj = obj.SerializeToJson();
            //Assert
            jsonObj.Should().NotBeNullOrEmpty();
            var CamelCaseField = "\"name\": \"Teste\"";
            var IgnoreCamelCaseField = "\"Name\": \"Teste\"";
            jsonObj.Should().NotContain(IgnoreCamelCaseField);
            jsonObj.Should().Contain(CamelCaseField);
        }

        [Test]
        public void DeserializeTo_ShouldBeConvertedToObject_WhenPassingCorrectObjectStringInJsonFormat()
        {
            //Arrange
            var obj = @"{
                          ""name"": ""Teste"",
                          ""time"": ""2020-06-29T17:49:31.3216846"",
                          ""enum"": 999,
                          ""nullValue"": null
                        }";
            //act
            var res = obj.DeserializeTo<Result>();
            //Assert
            res.Should().NotBeNull();
            res.Should().BeOfType(typeof(Result));
            res.Name.Should().BeEquivalentTo("Teste");

        }

        [Test]
        public void DeserializeTo_ShouldResultEmptyObject_WhenWrongJsonFormatOrWhenDoesntMeetTheParsingObject()
        {
            //Arrange
            var obj = @"{""OtherName"" : 12}";
            //act
            var res = obj.DeserializeTo<Result>();
            //Assert
            res.Name.Should().BeEquivalentTo(null);
            var defaultDate = default(DateTime);
            res.Time.Should().BeCloseTo(defaultDate);
        }

        [Test]
        public void DeserializeTo_ShouldThrowNullException_WhenTryToDeserializeNullObject()
        {
            //Arrange
            var obj = default(string);
            //act
            Action act = () => obj.DeserializeTo<Result>();
            //Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
