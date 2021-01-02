using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using AutoFixture;
using FluentMvvm.Internals;
using Moq;
using Shouldly;
using Xunit;

namespace FluentMvvm.Tests
{
    public class TypeHelperTest
    {
        [Fact]
        public void GetDefaultValue_Int_ReturnsDefaultInstance()
        {
            // Arrange
            var propertyMock = new Mock<PropertyInfo>();
            propertyMock.Setup(x => x.PropertyType).Returns(typeof(int));

            var property = propertyMock.Object;

            // Act
            object defaultValue = TypeHelper.GetDefaultValue(property);

            // Assert
            defaultValue.ShouldBe(default(int));
        }

        [Fact]
        public void GetDefaultValue_Struct_ReturnsDefaultInstance()
        {
            // Arrange
            var propertyMock = new Mock<PropertyInfo>();
            propertyMock.Setup(x => x.PropertyType).Returns(typeof(Point));

            var property = propertyMock.Object;

            // Act
            object defaultValue = TypeHelper.GetDefaultValue(property);

            // Assert
            defaultValue.ShouldBe(default(Point));
        }

        [Fact]
        public void GetDefaultValue_String_ReturnsNull()
        {
            // Arrange
            var propertyMock = new Mock<PropertyInfo>();
            propertyMock.Setup(x => x.PropertyType).Returns(typeof(string));

            var property = propertyMock.Object;

            // Act
            object defaultValue = TypeHelper.GetDefaultValue(property);

            // Assert
            defaultValue.ShouldBe(default(string));
        }

        [Fact]
        public void GetDefaultValue_IntWithDefaultValueAttribute_ReturnsAttributeValue()
        {
            // Arrange
            var defaultValue = new Fixture().Create<int>();

            var propertyMock = new Mock<PropertyInfo> { CallBase = true };
            propertyMock.Setup(x => x.PropertyType).Returns(typeof(int));

            // TypeHelper.GetDefaultValue uses CustomAttributeExtensions.GetCustomAttribute<T>() which cannot be mocked as it is an extension method.
            // So here we mock MemberInfo.GetCustomAttributes(bool) which will be called by the extension method.
            propertyMock.Setup(x => x.GetCustomAttributes(typeof(DefaultValueAttribute), It.IsAny<bool>())).Returns(new Attribute[] { new DefaultValueAttribute(defaultValue) });
            var property = propertyMock.Object;

            // Act
            object value = TypeHelper.GetDefaultValue(property);

            // Assert
            value.ShouldBe(defaultValue);
        }

        [Fact]
        public void GetDefaultValue_StringWithDefaultValueAttribute_ReturnsAttributeValue()
        {
            // Arrange
            var defaultValue = new Fixture().Create<string>();

            var propertyMock = new Mock<PropertyInfo> { CallBase = true };
            propertyMock.Setup(x => x.PropertyType).Returns(typeof(string));

            // TypeHelper.GetDefaultValue uses CustomAttributeExtensions.GetCustomAttribute<T>() which cannot be mocked as it is an extension method.
            // So here we mock MemberInfo.GetCustomAttributes(bool) which will be called by the extension method.
            propertyMock.Setup(x => x.GetCustomAttributes(typeof(DefaultValueAttribute), It.IsAny<bool>())).Returns(new Attribute[] { new DefaultValueAttribute(defaultValue) });
            var property = propertyMock.Object;

            // Act
            object value = TypeHelper.GetDefaultValue(property);

            // Assert
            value.ShouldBe(defaultValue);
        }

        [Theory]
        [InlineData(typeof(bool))]
        [InlineData(typeof(long))]
        public void DoesOverloadExist_PrimitiveType_ReturnsTrue(Type type)
        {
            TypeHelper.DoesOverloadExist(type).ShouldBeTrue();
        }

        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(DateTime))]
        [InlineData(typeof(decimal))]
        public void DoesOverloadExist_StringDecimalDateTime_ReturnsTrue(Type type)
        {
            TypeHelper.DoesOverloadExist(type).ShouldBeTrue();
        }

        [Theory]
        [InlineData(typeof(IWpfCommand))]
        [InlineData(typeof(DefaultValueAttribute))]
        public void DoesOverloadExist_RefType_ReturnsFalse(Type type)
        {
            TypeHelper.DoesOverloadExist(type).ShouldBeFalse();
        }

        [Theory]
        [InlineData(typeof(Point))]
        [InlineData(typeof(StringAlignment))]
        public void DoesOverloadExist_ValueType_ReturnsFalse(Type type)
        {
            TypeHelper.DoesOverloadExist(type).ShouldBeFalse();
        }
    }
}