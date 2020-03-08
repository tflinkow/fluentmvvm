using System;
using System.Windows.Input;

using FluentAssertions;

using FluentMvvm.Emit;
using FluentMvvm.Fluent;

using FluentMvvm.Tests.TestData;
using FluentMvvm.Tests.TestDataSources;
using FluentMvvm.Tests.Util;

using Microsoft.CSharp.RuntimeBinder;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class FluentViewModelBaseTests
    {
        [Theory]
        [InlineData(TestValues.Bool, nameof(TestViewModel.Bool))]
        [InlineData(TestValues.Byte, nameof(TestViewModel.Byte))]
        [InlineData(TestValues.SByte, nameof(TestViewModel.SByte))]
        [InlineData(TestValues.Char, nameof(TestViewModel.Char))]
        [InlineData(TestValues.Double, nameof(TestViewModel.Double))]
        [InlineData(TestValues.Float, nameof(TestViewModel.Float))]
        [InlineData(TestValues.Short, nameof(TestViewModel.Short))]
        [InlineData(TestValues.UShort, nameof(TestViewModel.UShort))]
        [InlineData(TestValues.Int, nameof(TestViewModel.Int))]
        [InlineData(TestValues.UInt, nameof(TestViewModel.UInt))]
        [InlineData(TestValues.Long, nameof(TestViewModel.Long))]
        [InlineData(TestValues.ULong, nameof(TestViewModel.ULong))]
        [InlineData(TestValues.String, nameof(TestViewModel.String))]
        [InlineData(TestValues.TestEnum, nameof(TestViewModel.TestEnum))]
        [MemberData(nameof(TestDataSource.DifferentValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Get_InitializedValues_ReturnsValues<T>(T value, string name)
        {
            // Arrange & Act
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Get<T>(name).Returns(value);

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            // Assert
            viewModel.Get<T>(name).Should().Be(value);
        }

        [Theory]
        [InlineData(default(bool), nameof(TestViewModel.Bool))]
        [InlineData(default(byte), nameof(TestViewModel.Byte))]
        [InlineData(default(sbyte), nameof(TestViewModel.SByte))]
        [InlineData(default(char), nameof(TestViewModel.Char))]
        [InlineData(default(double), nameof(TestViewModel.Double))]
        [InlineData(default(float), nameof(TestViewModel.Float))]
        [InlineData(default(short), nameof(TestViewModel.Short))]
        [InlineData(default(ushort), nameof(TestViewModel.UShort))]
        [InlineData(default(int), nameof(TestViewModel.Int))]
        [InlineData(default(uint), nameof(TestViewModel.UInt))]
        [InlineData(default(long), nameof(TestViewModel.Long))]
        [InlineData(default(ulong), nameof(TestViewModel.ULong))]
        [InlineData(default(string), nameof(TestViewModel.String))]
        [InlineData(default(TestEnum), nameof(TestViewModel.TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Get_NotInitializedValues_ReturnsDefault<T>(T value, string name)
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Get<T>(name).Returns(default(T));

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            // Act & Assert
            viewModel.Get<T>(name).Should().Be(value);
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
        [InlineData(default(string))]
        [InlineData(default(TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValues), MemberType = typeof(TestDataSource))]
        public void Get_NoPropertyOfName_Throws<T>(T _, string name = "not existing")
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Get<T>(name).Throws<ArgumentException>();

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            // Act & Assert
            FluentActions.Invoking(() => viewModel.Get<T>(name))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [Fact]
        public void GetOverloads_NoPropertyOfName_Throws()
        {

        }

        [Fact]
        public void GetOverloads_ReturnSameAsGeneric()
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Get<bool>(nameof(AllTypes.Bool)).Returns(TestValues.Bool);
            backingFieldProvider.Get<byte>(nameof(AllTypes.Byte)).Returns(TestValues.Byte);
            backingFieldProvider.Get<sbyte>(nameof(AllTypes.SByte)).Returns(TestValues.SByte);
            backingFieldProvider.Get<char>(nameof(AllTypes.Char)).Returns(TestValues.Char);
            backingFieldProvider.Get<decimal>(nameof(AllTypes.Decimal)).Returns(TestValues.Decimal);
            backingFieldProvider.Get<double>(nameof(AllTypes.Double)).Returns(TestValues.Double);
            backingFieldProvider.Get<float>(nameof(AllTypes.Float)).Returns(TestValues.Float);
            backingFieldProvider.Get<short>(nameof(AllTypes.Short)).Returns(TestValues.Short);
            backingFieldProvider.Get<ushort>(nameof(AllTypes.UShort)).Returns(TestValues.UShort);
            backingFieldProvider.Get<int>(nameof(AllTypes.Int)).Returns(TestValues.Int);
            backingFieldProvider.Get<uint>(nameof(AllTypes.UInt)).Returns(TestValues.UInt);
            backingFieldProvider.Get<long>(nameof(AllTypes.Long)).Returns(TestValues.Long);
            backingFieldProvider.Get<ulong>(nameof(AllTypes.ULong)).Returns(TestValues.ULong);
            backingFieldProvider.Get<DateTime>(nameof(AllTypes.DateTime)).Returns(TestValues.DateTime);

            backingFieldProvider.GetBool(nameof(AllTypes.Bool)).Returns(TestValues.Bool);
            backingFieldProvider.GetByte(nameof(AllTypes.Byte)).Returns(TestValues.Byte);
            backingFieldProvider.GetSByte(nameof(AllTypes.SByte)).Returns(TestValues.SByte);
            backingFieldProvider.GetChar(nameof(AllTypes.Char)).Returns(TestValues.Char);
            backingFieldProvider.GetDecimal(nameof(AllTypes.Decimal)).Returns(TestValues.Decimal);
            backingFieldProvider.GetDouble(nameof(AllTypes.Double)).Returns(TestValues.Double);
            backingFieldProvider.GetFloat(nameof(AllTypes.Float)).Returns(TestValues.Float);
            backingFieldProvider.GetInt16(nameof(AllTypes.Short)).Returns(TestValues.Short);
            backingFieldProvider.GetUInt16(nameof(AllTypes.UShort)).Returns(TestValues.UShort);
            backingFieldProvider.GetInt32(nameof(AllTypes.Int)).Returns(TestValues.Int);
            backingFieldProvider.GetUInt32(nameof(AllTypes.UInt)).Returns(TestValues.UInt);
            backingFieldProvider.GetInt64(nameof(AllTypes.Long)).Returns(TestValues.Long);
            backingFieldProvider.GetUInt64(nameof(AllTypes.ULong)).Returns(TestValues.ULong);
            backingFieldProvider.GetDateTime(nameof(AllTypes.DateTime)).Returns(TestValues.DateTime);


            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            // Assert
            viewModel.GetBool(nameof(AllTypes.Bool)).Should().Be(viewModel.Get<bool>(nameof(AllTypes.Bool)));
            viewModel.GetByte(nameof(AllTypes.Byte)).Should().Be(viewModel.Get<byte>(nameof(AllTypes.Byte)));
            viewModel.GetSByte(nameof(AllTypes.SByte)).Should().Be(viewModel.Get<sbyte>(nameof(AllTypes.SByte)));
            viewModel.GetChar(nameof(AllTypes.Char)).Should().Be(viewModel.Get<char>(nameof(AllTypes.Char)));
            viewModel.GetDecimal(nameof(AllTypes.Decimal)).Should().Be(viewModel.Get<decimal>(nameof(AllTypes.Decimal)));
            viewModel.GetDouble(nameof(AllTypes.Double)).Should().Be(viewModel.Get<double>(nameof(AllTypes.Double)));
            viewModel.GetFloat(nameof(AllTypes.Float)).Should().Be(viewModel.Get<float>(nameof(AllTypes.Float)));
            viewModel.GetInt16(nameof(AllTypes.Short)).Should().Be(viewModel.Get<short>(nameof(AllTypes.Short)));
            viewModel.GetUInt16(nameof(AllTypes.UShort)).Should().Be(viewModel.Get<ushort>(nameof(AllTypes.UShort)));
            viewModel.GetInt32(nameof(AllTypes.Int)).Should().Be(viewModel.Get<int>(nameof(AllTypes.Int)));
            viewModel.GetUInt32(nameof(AllTypes.UInt)).Should().Be(viewModel.Get<uint>(nameof(AllTypes.UInt)));
            viewModel.GetInt64(nameof(AllTypes.Long)).Should().Be(viewModel.Get<long>(nameof(AllTypes.Long)));
            viewModel.GetUInt64(nameof(AllTypes.ULong)).Should().Be(viewModel.Get<ulong>(nameof(AllTypes.ULong)));
            viewModel.GetString(nameof(AllTypes.String)).Should().Be(viewModel.Get<string>(nameof(AllTypes.String)));
            viewModel.GetDateTime(nameof(AllTypes.DateTime)).Should().Be(viewModel.Get<DateTime>(nameof(AllTypes.DateTime)));
        }

        [Theory]
        [InlineData(TestValues.Bool, nameof(TestViewModel.Bool))]
        [InlineData(TestValues.Byte, nameof(TestViewModel.Byte))]
        [InlineData(TestValues.SByte, nameof(TestViewModel.SByte))]
        [InlineData(TestValues.Char, nameof(TestViewModel.Char))]
        [InlineData(TestValues.Double, nameof(TestViewModel.Double))]
        [InlineData(TestValues.Float, nameof(TestViewModel.Float))]
        [InlineData(TestValues.Short, nameof(TestViewModel.Short))]
        [InlineData(TestValues.UShort, nameof(TestViewModel.UShort))]
        [InlineData(TestValues.Int, nameof(TestViewModel.Int))]
        [InlineData(TestValues.UInt, nameof(TestViewModel.UInt))]
        [InlineData(TestValues.Long, nameof(TestViewModel.Long))]
        [InlineData(TestValues.ULong, nameof(TestViewModel.ULong))]
        [InlineData(TestValues.String, nameof(TestViewModel.String))]
        [InlineData(TestValues.TestEnum, nameof(TestViewModel.TestEnum))]
        [MemberData(nameof(TestDataSource.DifferentValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Set_DifferentValue_RaisesEvent<T>(T value, string name)
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Set(value, name).Returns(true);

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);
            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(value, name);

            // Assert
            listener.Received.Count.Should().Be(1);
            listener.Received.Should().BeEquivalentTo(name);
        }

        [Fact]
        public void SetOverload_DifferentValue_RaisesEvent()
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Set(TestValues.Bool, nameof(AllTypes.Bool)).Returns(true);
            backingFieldProvider.Set(TestValues.Byte, nameof(AllTypes.Byte)).Returns(true);
            backingFieldProvider.Set(TestValues.SByte, nameof(AllTypes.SByte)).Returns(true);
            backingFieldProvider.Set(TestValues.Char, nameof(AllTypes.Char)).Returns(true);
            backingFieldProvider.Set(TestValues.Decimal, nameof(AllTypes.Decimal)).Returns(true);
            backingFieldProvider.Set(TestValues.Double, nameof(AllTypes.Double)).Returns(true);
            backingFieldProvider.Set(TestValues.Float, nameof(AllTypes.Float)).Returns(true);
            backingFieldProvider.Set(TestValues.Short, nameof(AllTypes.Short)).Returns(true);
            backingFieldProvider.Set(TestValues.UShort, nameof(AllTypes.UShort)).Returns(true);
            backingFieldProvider.Set(TestValues.Int, nameof(AllTypes.Int)).Returns(true);
            backingFieldProvider.Set(TestValues.UInt, nameof(AllTypes.UInt)).Returns(true);
            backingFieldProvider.Set(TestValues.Long, nameof(AllTypes.Long)).Returns(true);
            backingFieldProvider.Set(TestValues.ULong, nameof(AllTypes.ULong)).Returns(true);
            backingFieldProvider.Set(TestValues.String, nameof(AllTypes.String)).Returns(true);
            backingFieldProvider.Set(TestValues.DateTime, nameof(AllTypes.DateTime)).Returns(true);

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(TestValues.Bool, nameof(AllTypes.Bool));
            viewModel.Set(TestValues.Byte, nameof(AllTypes.Byte));
            viewModel.Set(TestValues.SByte, nameof(AllTypes.SByte));
            viewModel.Set(TestValues.Char, nameof(AllTypes.Char));
            viewModel.Set(TestValues.Decimal, nameof(AllTypes.Decimal));
            viewModel.Set(TestValues.Double, nameof(AllTypes.Double));
            viewModel.Set(TestValues.Float, nameof(AllTypes.Float));
            viewModel.Set(TestValues.Short, nameof(AllTypes.Short));
            viewModel.Set(TestValues.UShort, nameof(AllTypes.UShort));
            viewModel.Set(TestValues.Int, nameof(AllTypes.Int));
            viewModel.Set(TestValues.UInt, nameof(AllTypes.UInt));
            viewModel.Set(TestValues.Long, nameof(AllTypes.Long));
            viewModel.Set(TestValues.ULong, nameof(AllTypes.ULong));
            viewModel.Set(TestValues.String, nameof(AllTypes.String));
            viewModel.Set(TestValues.DateTime, nameof(AllTypes.DateTime));

            // Assert
            listener.Received.Count.Should().Be(15);
            listener.Received.Should().BeEquivalentTo(new[]
            {
                nameof(AllTypes.Bool), nameof(AllTypes.Byte), nameof(AllTypes.SByte), 
                nameof(AllTypes.Char), nameof(AllTypes.Decimal), nameof(AllTypes.Double),
                nameof(AllTypes.Float), nameof(AllTypes.Short), nameof(AllTypes.UShort),
                nameof(AllTypes.Int), nameof(AllTypes.UInt), nameof(AllTypes.Long),
                nameof(AllTypes.ULong), nameof(AllTypes.String), nameof(AllTypes.DateTime),
            });
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
        [InlineData(default(string), nameof(AllTypes.String))]
        [InlineData(default(TestEnum), nameof(AllTypes.TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValuesWithNames), MemberType = typeof(TestDataSource))]
        public void Set_SameValue_DoesNothing<T>(T value, string name)
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Set(value, name).Returns(false);

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);
            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(value, name);

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void SetOverload_SameValue_DoesNotRaiseEvent()
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Set(TestValues.Bool, nameof(AllTypes.Bool)).Returns(false);
            backingFieldProvider.Set(TestValues.Byte, nameof(AllTypes.Byte)).Returns(false);
            backingFieldProvider.Set(TestValues.SByte, nameof(AllTypes.SByte)).Returns(false);
            backingFieldProvider.Set(TestValues.Char, nameof(AllTypes.Char)).Returns(false);
            backingFieldProvider.Set(TestValues.Decimal, nameof(AllTypes.Decimal)).Returns(false);
            backingFieldProvider.Set(TestValues.Double, nameof(AllTypes.Double)).Returns(false);
            backingFieldProvider.Set(TestValues.Float, nameof(AllTypes.Float)).Returns(false);
            backingFieldProvider.Set(TestValues.Short, nameof(AllTypes.Short)).Returns(false);
            backingFieldProvider.Set(TestValues.UShort, nameof(AllTypes.UShort)).Returns(false);
            backingFieldProvider.Set(TestValues.Int, nameof(AllTypes.Int)).Returns(false);
            backingFieldProvider.Set(TestValues.UInt, nameof(AllTypes.UInt)).Returns(false);
            backingFieldProvider.Set(TestValues.Long, nameof(AllTypes.Long)).Returns(false);
            backingFieldProvider.Set(TestValues.ULong, nameof(AllTypes.ULong)).Returns(false);
            backingFieldProvider.Set(TestValues.String, nameof(AllTypes.String)).Returns(false);
            backingFieldProvider.Set(TestValues.DateTime, nameof(AllTypes.DateTime)).Returns(false);

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(TestValues.Bool, nameof(AllTypes.Bool));
            viewModel.Set(TestValues.Byte, nameof(AllTypes.Byte));
            viewModel.Set(TestValues.SByte, nameof(AllTypes.SByte));
            viewModel.Set(TestValues.Char, nameof(AllTypes.Char));
            viewModel.Set(TestValues.Decimal, nameof(AllTypes.Decimal));
            viewModel.Set(TestValues.Double, nameof(AllTypes.Double));
            viewModel.Set(TestValues.Float, nameof(AllTypes.Float));
            viewModel.Set(TestValues.Short, nameof(AllTypes.Short));
            viewModel.Set(TestValues.UShort, nameof(AllTypes.UShort));
            viewModel.Set(TestValues.Int, nameof(AllTypes.Int));
            viewModel.Set(TestValues.UInt, nameof(AllTypes.UInt));
            viewModel.Set(TestValues.Long, nameof(AllTypes.Long));
            viewModel.Set(TestValues.ULong, nameof(AllTypes.ULong));
            viewModel.Set(TestValues.String, nameof(AllTypes.String));
            viewModel.Set(TestValues.DateTime, nameof(AllTypes.DateTime));

            // Assert
            listener.Received.Count.Should().Be(0);
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
        [InlineData(default(string))]
        [InlineData(default(TestEnum))]
        [MemberData(nameof(TestDataSource.DefaultValues), MemberType = typeof(TestDataSource))]
        public void Set_NoPropertyOfName_Throws<T>(T value, string name = "not existing") // TODO: this test also for overloads
        {
            // Arrange
            IBackingFieldProvider backingFieldProvider = Substitute.For<IBackingFieldProvider>();
            backingFieldProvider.Set(value, name).Throws<ArgumentException>();

            TestViewModel viewModel = new TestViewModel(backingFieldProvider);

            // Act & Assert
            FluentActions.Invoking(() => viewModel.Set(value, name))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [Fact]
        public void When_False_ReturnsEmptyFluentAction()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());

            // Act & Assert
            viewModel.When(false).Should().BeOfType<EmptyFluentAction>();
            viewModel.When(() => false).Should().BeOfType<EmptyFluentAction>();
        }

        [Fact]
        public void When_True_ReturnsSelf()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());

            // Act & Assert
            viewModel.When(true).Should().BeOfType<TestViewModel>();
            viewModel.When(true).Should().BeSameAs(viewModel);
            viewModel.When(() => true).Should().BeOfType<TestViewModel>();
            viewModel.When(() => true).Should().BeSameAs(viewModel);
        }

        [Fact]
        public void When_FuncNull_Throws()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());

            // Act & Assert
            FluentActions.Invoking(() => viewModel.When(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WasUpdated_IsTrue()
        {
            // Arrange
            IDependencyExpression viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());

            // Act & Assert
            viewModel.WasUpdated().Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(TestDataSource.CommandsWithRaiseCanExecuteChanged), MemberType = typeof(TestDataSource))]
        public void AffectsCommand_CommandsWithRaiseCanExecuteChanged_RaisesEvent(ICommand command)
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel;

            EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

            // Act
            fluentAction.Affects(command);

            // Assert
            listener.Received.Count.Should().Be(1);
        }

        [Fact]
        public void AffectsCommand_NoRaiseCanExecuteChanged_Throws()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel;

            TestICommandNoRaiseMethod command = new TestICommandNoRaiseMethod();

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects(command)).Should().Throw<RuntimeBinderException>();
        }

        [Fact]
        public void AffectsCommand_Null_DoesNotThrow()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel;

            ICommand command = null;

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects(command)).Should().NotThrow();
        }

        [Fact]
        public void AffectsProperty_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel;

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Affects("X");

            // Assert
            listener.Received.Count.Should().Be(1);
            listener.Received[0].Should().Be("X");
        }
    }
}