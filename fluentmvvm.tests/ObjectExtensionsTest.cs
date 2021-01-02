using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentMvvm.Internals;
using Shouldly;
using Xunit;

namespace FluentMvvm.Tests
{
    public class ObjectExtensionsTest
    {
        [Theory]
        [AutoData]
        public void NotNull_ShouldThrowArgumentNullException_IfValueIsNull(string parameterName)
        {
            Should.Throw<ArgumentNullException>(() => ObjectExtensions.NotNull<object>(null, parameterName)).Message.ShouldContain(parameterName);
        }

        [Fact]
        public void NotNull_ShouldReturnValue_IfValueIsNotNull()
        {
            var fixture = new Fixture();

            string name = fixture.Create<string>();
            string refValue = fixture.Create<string>();
            int value = fixture.Create<int>();

            refValue.NotNull(name).ShouldBeSameAs(refValue);
            value.NotNull(name).ShouldBe(value);
        }

        [Theory]
        [InlineAutoData((string) null)]
        [InlineAutoData("")]
        [InlineAutoData("  ")]
        public void NotNullOrEmptyOrWhiteSpace_ShouldThrowArgumentException_IfValueIsNullEmptyOrWhiteSpace(string value, string parameterName)
        {
            ArgumentException exception = Should.Throw<ArgumentException>(() => value.NotNullOrEmptyOrWhiteSpace(parameterName));
            exception.Message.ShouldContain(parameterName);
            exception.Message.ShouldContain("null");
            exception.Message.ShouldContain("empty");
            exception.Message.ShouldContain("white-space characters");
        }

        [Theory]
        [AutoData]
        public void NotNullOrEmptyOrWhiteSpace_ShouldReturnValue_IfValueIsNotNullOrEmptyOrWhiteSpace(string value, string parameterName)
        {
            value.NotNullOrEmptyOrWhiteSpace(parameterName).ShouldBeSameAs(value);
        }
    }
}