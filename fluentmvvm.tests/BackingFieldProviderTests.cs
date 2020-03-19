using System;
using System.Diagnostics;
using System.Reflection;

using FluentAssertions;

using FluentMvvm.Emit;

using FluentMvvm.Tests.TestData;
using FluentMvvm.Tests.TestDataSources;

using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class BackingFieldProviderTests
    {
        [Theory]
        [InlineData(nameof(FieldGenerationLimited.PublicInstance))]
        [InlineData(nameof(FieldGenerationLimited.PublicWritableInstanceSuppressed))]
        [InlineData(nameof(FieldGenerationLimited.PublicWritableStatic))]
        [InlineData("PrivateWritableInstance")]
        [InlineData("TotallyNotExisting")]
        public void GeneratedType_HasBackingFields_OnlyForPublicWritableInstanceProperties(string name)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(FieldGenerationLimited));

            // Act
            FieldInfo[] backingFields = provider.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            // Assert
            backingFields.Length.Should().Be(1);
            backingFields[0].Name.Should().Be(nameof(FieldGenerationLimited.PublicWritableInstance));

            FluentActions.Invoking(() => provider.GetInt32(name))
                         .Should()
                         .Throw<ArgumentException>()
                         .WithMessage("Cannot find a public writable instance property of type '*' named '*'.");
        }

        [Theory]
        [InlineData(default(bool), nameof(AllTypes.Bool))]
        [InlineData(default(byte), nameof(AllTypes.Byte))]
        [InlineData(default(sbyte), nameof(AllTypes.SByte))]
        [InlineData(default(char), nameof(AllTypes.Char))]
        [InlineData(default(double), nameof(AllTypes.Double))]
        [InlineData(default(float), nameof(AllTypes.Float))]
        [InlineData(default(short), nameof(AllTypes.Short))]
        [InlineData(default(ushort), nameof(AllTypes.UShort))]
        [InlineData(default(int), nameof(AllTypes.Int))]
        [InlineData(default(uint), nameof(AllTypes.UInt))]
        [InlineData(default(long), nameof(AllTypes.Long))]
        [InlineData(default(ulong), nameof(AllTypes.ULong))]
        [InlineData("", nameof(AllTypes.String))]
        [InlineData(default(TestClass), nameof(AllTypes.TestClass))]
        [InlineData(default(TestEnum), nameof(AllTypes.TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValuesWithNames), MemberType = typeof(TestDataSource))]
        public void GeneratedType_HasNoBackingFields_WhenFieldGenerationIsSuppressedForType<T>(T _, string name)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(NoGenerationTestClass));

            // Act
            FieldInfo[] backingFields = provider.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            // Assert
            backingFields.Should().BeEmpty();

            FluentActions.Invoking(() => provider.Get<T>(name))
                         .Should()
                         .Throw<InvalidOperationException>()
                         .WithMessage("Backing field generation is suppressed for type '*'.");
        }

        [Fact]
        public void GeneratingType_WithDifferentGenericParameters_DoesNotThrow()
        {
            // Arrange
            BackingFieldProvider.Get(typeof(TestViewModelGeneric<int>));

            // Act & Assert
            FluentActions.Invoking(() => BackingFieldProvider.Get(typeof(TestViewModelGeneric<string>)))
                         .Should()
                         .NotThrow();
        }

        [Theory]
        [InlineData(default(bool))]
        [InlineData(default(byte))]
        [InlineData(default(sbyte))]
        [InlineData(default(char))]
        [InlineData(default(double))]
        [InlineData(default(float))]
        [InlineData(default(short))]
        [InlineData(default(ushort))]
        [InlineData(default(int))]
        [InlineData(default(uint))]
        [InlineData(default(long))]
        [InlineData(default(ulong))]
        [InlineData("")]
        [InlineData(default(TestClass))]
        [InlineData(default(TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValues), MemberType = typeof(TestDataSource))]
        public void Get_NoPropertyOfName_Throws<T>(T _)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            // Act & Assert
            FluentActions.Invoking(() => provider.Get<T>("X")).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(default(bool), nameof(AllTypes.Bool))]
        [InlineData(default(byte), nameof(AllTypes.Byte))]
        [InlineData(default(sbyte), nameof(AllTypes.SByte))]
        [InlineData(default(char), nameof(AllTypes.Char))]
        [InlineData(default(double), nameof(AllTypes.Double))]
        [InlineData(default(float), nameof(AllTypes.Float))]
        [InlineData(default(short), nameof(AllTypes.Short))]
        [InlineData(default(ushort), nameof(AllTypes.UShort))]
        [InlineData(default(int), nameof(AllTypes.Int))]
        [InlineData(default(uint), nameof(AllTypes.UInt))]
        [InlineData(default(long), nameof(AllTypes.Long))]
        [InlineData(default(ulong), nameof(AllTypes.ULong))]
        [InlineData("", nameof(AllTypes.String))]
        [InlineData(default(TestClass), nameof(AllTypes.TestClass))]
        [InlineData(default(TestEnum), nameof(AllTypes.TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Get_Uninitialized_ReturnsDefaultValue<T>(T defaultValue, string name, T stringDefault = default)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            // Hack, because when passing default(string) as InlineData, T is object.
            // Thus we pass "" for type inference to work correctly, but then the default value for string is incorrect.
            if (defaultValue is string)
            {
                defaultValue = stringDefault;
            }

            // Act & Assert
            provider.Get<T>(name).Should().Be(defaultValue);
        }

        [Theory]
        [InlineData(default(bool), nameof(AllTypes.Bool), true)]
        [InlineData(default(byte), nameof(AllTypes.Byte), 120)]
        [InlineData(default(sbyte), nameof(AllTypes.SByte), -123)]
        [InlineData(default(char), nameof(AllTypes.Char), '?')]
        [InlineData(default(double), nameof(AllTypes.Double), 123.45)]
        [InlineData(default(float), nameof(AllTypes.Float), 321.54f)]
        [InlineData(default(short), nameof(AllTypes.Short), -9000)]
        [InlineData(default(ushort), nameof(AllTypes.UShort), 10000)]
        [InlineData(default(int), nameof(AllTypes.Int), -123456789)]
        [InlineData(default(uint), nameof(AllTypes.UInt), 987654321)]
        [InlineData(default(long), nameof(AllTypes.Long), -1000000000)]
        [InlineData(default(ulong), nameof(AllTypes.ULong), 2000000000)]
        [InlineData("", nameof(AllTypes.String), "Hello World")]
        [InlineData(default(TestEnum), nameof(AllTypes.TestEnum), TestEnum.Red)]
        public void Get_TakesDefaultValueIntoAccount<T>(T _, string name, object overriddenDefaultValue)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypesWithDefaultValues));

            // Act & Arrange
            provider.Get<T>(name).Should().Be(overriddenDefaultValue);
        }

        [Fact]
        public void GetOverloads_ReturnSameAsGeneric()
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            void AssumeEquality()
            {
                provider.GetBool(nameof(AllTypes.Bool)).Should().Be(provider.Get<bool>(nameof(AllTypes.Bool)));
                provider.GetByte(nameof(AllTypes.Byte)).Should().Be(provider.Get<byte>(nameof(AllTypes.Byte)));
                provider.GetSByte(nameof(AllTypes.SByte)).Should().Be(provider.Get<sbyte>(nameof(AllTypes.SByte)));
                provider.GetChar(nameof(AllTypes.Char)).Should().Be(provider.Get<char>(nameof(AllTypes.Char)));
                provider.GetDecimal(nameof(AllTypes.Decimal)).Should().Be(provider.Get<decimal>(nameof(AllTypes.Decimal)));
                provider.GetDouble(nameof(AllTypes.Double)).Should().Be(provider.Get<double>(nameof(AllTypes.Double)));
                provider.GetFloat(nameof(AllTypes.Float)).Should().Be(provider.Get<float>(nameof(AllTypes.Float)));
                provider.GetInt16(nameof(AllTypes.Short)).Should().Be(provider.Get<short>(nameof(AllTypes.Short)));
                provider.GetUInt16(nameof(AllTypes.UShort)).Should().Be(provider.Get<ushort>(nameof(AllTypes.UShort)));
                provider.GetInt32(nameof(AllTypes.Int)).Should().Be(provider.Get<int>(nameof(AllTypes.Int)));
                provider.GetUInt32(nameof(AllTypes.UInt)).Should().Be(provider.Get<uint>(nameof(AllTypes.UInt)));
                provider.GetInt64(nameof(AllTypes.Long)).Should().Be(provider.Get<long>(nameof(AllTypes.Long)));
                provider.GetUInt64(nameof(AllTypes.ULong)).Should().Be(provider.Get<ulong>(nameof(AllTypes.ULong)));
                provider.GetString(nameof(AllTypes.String)).Should().Be(provider.Get<string>(nameof(AllTypes.String)));
                provider.GetDateTime(nameof(AllTypes.DateTime)).Should().Be(provider.Get<DateTime>(nameof(AllTypes.DateTime)));
            }

            // Assume
            AssumeEquality();

            // Act
            provider.Set(TestValues.Bool, nameof(AllTypes.Bool));
            provider.Set(TestValues.Byte, nameof(AllTypes.Byte));
            provider.Set(TestValues.SByte, nameof(AllTypes.SByte));
            provider.Set(TestValues.Char, nameof(AllTypes.Char));
            provider.Set(TestValues.Decimal, nameof(AllTypes.Decimal));
            provider.Set(TestValues.Double, nameof(AllTypes.Double));
            provider.Set(TestValues.Float, nameof(AllTypes.Float));
            provider.Set(TestValues.Short, nameof(AllTypes.Short));
            provider.Set(TestValues.UShort, nameof(AllTypes.UShort));
            provider.Set(TestValues.Int, nameof(AllTypes.Int));
            provider.Set(TestValues.UInt, nameof(AllTypes.UInt));
            provider.Set(TestValues.Long, nameof(AllTypes.Long));
            provider.Set(TestValues.ULong, nameof(AllTypes.ULong));
            provider.Set(TestValues.String, nameof(AllTypes.String));
            provider.Set(TestValues.DateTime, nameof(AllTypes.DateTime));

            // Assert
            AssumeEquality();
        }

        [Theory]
        [InlineData(default(long), nameof(AllTypes.Float))]
        [InlineData(default(int), nameof(AllTypes.Byte))]
        [InlineData(default(byte), nameof(AllTypes.Int))]
        [InlineData(default(int), nameof(AllTypes.String))] // TODO: this throws a NullRefEx when String is null
        [InlineData("", nameof(AllTypes.UInt))]
        [InlineData(default(bool), nameof(AllTypes.DateTime))]
        [InlineData(default(TestEnum), nameof(AllTypes.Int))] // TODO: this works, but should it?
        public void Get_WrongType_Throws<T>(T _, string name)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            // Act & Assert
            FluentActions.Invoking(() => provider.Get<T>(name))
                         .Should()
                         .Throw<InvalidCastException>();
        }

        [Theory]
        [InlineData(default(bool), TestValues.Bool, nameof(AllTypes.Bool))]
        [InlineData(default(byte), TestValues.Byte, nameof(AllTypes.Byte))]
        [InlineData(default(sbyte), TestValues.SByte, nameof(AllTypes.SByte))]
        [InlineData(default(char), TestValues.Char, nameof(AllTypes.Char))]
        [InlineData(default(double), TestValues.Double, nameof(AllTypes.Double))]
        [InlineData(default(float), TestValues.Float, nameof(AllTypes.Float))]
        [InlineData(default(short), TestValues.Short, nameof(AllTypes.Short))]
        [InlineData(default(ushort), TestValues.UShort, nameof(AllTypes.UShort))]
        [InlineData(default(int), TestValues.Int, nameof(AllTypes.Int))]
        [InlineData(default(uint), TestValues.UInt, nameof(AllTypes.UInt))]
        [InlineData(default(long), TestValues.Long, nameof(AllTypes.Long))]
        [InlineData(default(ulong), TestValues.ULong, nameof(AllTypes.ULong))]
        [InlineData("", TestValues.String, nameof(AllTypes.String))]
        [InlineData(default(TestEnum), TestValues.TestEnum, nameof(AllTypes.TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValuesWithDifferentValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Set_ReplacesValue<T>(T before, T after, string name, T stringDefault = default)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            // Hack, because when passing default(string) as InlineData, T is object.
            // Thus we pass "" for type inference to work correctly, but then the default value for string is incorrect.
            if (before is string)
            {
                before = stringDefault;
            }

            // Assume
            provider.Get<T>(name).Should().Be(before);

            // Act
            provider.Set(after, name);

            // Assert
            provider.Get<T>(name).Should().Be(after);
        }

        [Theory]
        [InlineData(default(bool))]
        [InlineData(default(byte))]
        [InlineData(default(sbyte))]
        [InlineData(default(char))]
        [InlineData(default(double))]
        [InlineData(default(float))]
        [InlineData(default(short))]
        [InlineData(default(ushort))]
        [InlineData(default(int))]
        [InlineData(default(uint))]
        [InlineData(default(long))]
        [InlineData(default(ulong))]
        [InlineData("")]
        [InlineData(default(TestClass))]
        [InlineData(default(TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValues), MemberType = typeof(TestDataSource))]
        public void Set_NoPropertyOfName_Throws<T>(T value)
        {
            // Arrange
            IBackingFieldProvider provider = BackingFieldProvider.Get(typeof(AllTypes));

            // Act & Assert
            FluentActions.Invoking(() => provider.Set<T>(value, "X")).Should().Throw<ArgumentException>();
        }
    }
}