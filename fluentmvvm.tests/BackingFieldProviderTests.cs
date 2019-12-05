using System;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using FluentMvvm.Tests.Models;

using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class BackingFieldProviderTests
    {
        [Fact]
        public void When_NoPublicWritableInstanceProperties_GeneratedType_IsNull()
        {
            // Arrange & Act
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(EmptyClass));

            // Assert
            provider.Should().BeNull();
        }

        [Fact]
        public void GeneratedType_HasBackingFields_OnlyForPublicWritableInstanceProperties()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));

            // Act
            var backingFields = provider.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            // Assert
            backingFields.Length.Should().Be(5);
            backingFields.Count(x => x.Name == nameof(TestClass.Internal)).Should().Be(0);
            backingFields.Count(x => x.Name == nameof(TestClass.GetOnly)).Should().Be(0);
        }

        [Fact]
        public void GetValueOf_Uninitialized_ReturnsDefaultValue()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));

            // Act & Assert
            provider.GetValueOf(nameof(TestClass.A)).Should().Be(default(int));
            provider.GetValueOf(nameof(TestClass.B)).Should().Be(default(int?));
            provider.GetValueOf(nameof(TestClass.C)).Should().Be(default(TestClass));
            provider.GetValueOf(nameof(TestClass.D)).Should().Be(default(TestStruct));
            provider.GetValueOf(nameof(TestClass.E)).Should().Be(default(string));
        }

        [Fact]
        public void GetValueOf_ExistingProperty_ReturnsValue()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));
            provider.SetValueOf(nameof(TestClass.A), 42);

            // Act & Assert
            provider.GetValueOf(nameof(TestClass.A)).Should().Be(42);
        }

        [Fact]
        public void GetValueOf_NotExistingProperty_Throws()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));

            // Act & Assert
            FluentActions.Invoking(() => provider.GetValueOf("-")).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SetValueOf_ExistingProperty_ReplacesValue()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));

            // Assume
            provider.GetValueOf(nameof(TestClass.A)).Should().Be(default(int));

            // Act
            provider.SetValueOf(nameof(TestClass.A), 42);

            // Assert
            provider.GetValueOf(nameof(TestClass.A)).Should().Be(42);
        }

        [Fact]
        public void SetValueOf_NotExistingProperty_Throws()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(TestClass));

            // Act & Assert
            FluentActions.Invoking(() => provider.SetValueOf("-", 0)).Should().Throw<ArgumentException>();
        }
    }
}