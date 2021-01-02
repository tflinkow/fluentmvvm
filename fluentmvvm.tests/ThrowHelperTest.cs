using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture.Xunit2;
using FluentMvvm.Internals;
using Shouldly;
using Xunit;

namespace FluentMvvm.Tests
{
    public class ThrowHelperTest
    {
        [Theory]
        [AutoData]
        public void ThrowArgumentNullException_ShouldThrow(string parameterName)
        {
            Should.Throw<ArgumentNullException>(() => ThrowHelper.ThrowArgumentNullException(parameterName)).Message.ShouldContain(parameterName);
        }

        [Theory]
        [AutoData]
        public void ThrowArgumentNullEmptyOrWhiteSpaceException_ShouldThrow(string parameterName)
        {
            ArgumentException exception = Should.Throw<ArgumentException>(() => ThrowHelper.ThrowArgumentNullEmptyOrWhiteSpace(parameterName));
            exception.Message.ShouldContain(parameterName);
            exception.Message.ShouldContain("null");
            exception.Message.ShouldContain("empty");
            exception.Message.ShouldContain("white-space characters");
        }

        [Theory]
        [AutoData]
        public void ThrowKeyNotFoundException_ShouldThrow(string key)
        {
            KeyNotFoundException exception = Should.Throw<KeyNotFoundException>(() => ThrowHelper.ThrowKeyNotFoundException(key));
            exception.Message.ShouldContain(key);
            exception.Message.ShouldContain("not present");
        }

        [Theory]
        [AutoData]
        public void ThrowNoBackingFieldsOfType_NoBackingFieldCreation_ShouldThrow(Type onType)
        {
            var exception = Should.Throw<InvalidOperationException>(() => ThrowHelper.ThrowNoBackingFieldsOfType<int>(onType));
            exception.Message.ShouldContain(typeof(int).Name);
            exception.Message.ShouldContain(onType.ToString());
            exception.Message.ShouldContain("backing fields");
            exception.Message.ShouldContain("creation");
        }

        [Theory]
        [AutoData]
        public void ThrowPropertyNotFound_ShouldThrow(string propertyName, Type propertyType, Type onType)
        {
            ArgumentException exception = Should.Throw<ArgumentException>(() => ThrowHelper.ThrowPropertyNotFound(propertyName, propertyType, onType));
            exception.Message.ShouldContain(propertyName);
            exception.Message.ShouldContain(propertyType.ToString());
            exception.Message.ShouldContain(onType.ToString());
            exception.Message.ShouldContain("public writable instance property");
        }
    }
}
