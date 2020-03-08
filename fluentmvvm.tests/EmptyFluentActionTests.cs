using System;
using System.Windows.Input;

using FluentAssertions;

using FluentMvvm.Emit;
using FluentMvvm.Fluent;

using FluentMvvm.Tests.TestData;
using FluentMvvm.Tests.TestDataSources;
using FluentMvvm.Tests.Util;

using NSubstitute;

using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class EmptyFluentActionTests
    {
        [Fact]
        public void WasUpdated_IsFalse()
        {
            // Arrange
            EmptyFluentAction fluentAction = EmptyFluentAction.Default;

            // Act & Assert
            fluentAction.WasUpdated().Should().BeFalse();
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
        public void Set_DifferentValue_DoesNotRaiseEvent<T>(T value, string name)
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(value, name);

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void SetOverloads_DifferentValue_DoesNotRaiseEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(TestValues.Bool, nameof(AllTypes.Bool));
            fluentAction.Set(TestValues.Byte, nameof(AllTypes.Byte));
            fluentAction.Set(TestValues.SByte, nameof(AllTypes.SByte));
            fluentAction.Set(TestValues.Char, nameof(AllTypes.Char));
            fluentAction.Set(TestValues.Decimal, nameof(AllTypes.Decimal));
            fluentAction.Set(TestValues.Double, nameof(AllTypes.Double));
            fluentAction.Set(TestValues.Float, nameof(AllTypes.Float));
            fluentAction.Set(TestValues.Short, nameof(AllTypes.Short));
            fluentAction.Set(TestValues.UShort, nameof(AllTypes.UShort));
            fluentAction.Set(TestValues.Int, nameof(AllTypes.Int));
            fluentAction.Set(TestValues.UInt, nameof(AllTypes.UInt));
            fluentAction.Set(TestValues.Long, nameof(AllTypes.Long));
            fluentAction.Set(TestValues.ULong, nameof(AllTypes.ULong));
            fluentAction.Set(TestValues.String, nameof(AllTypes.String));
            fluentAction.Set(TestValues.DateTime, nameof(AllTypes.DateTime));

            // Assert
            listener.Received.Count.Should().Be(0);
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
        public void Set_SameValue_DoesNotRaiseEvent<T>(T value, string name)
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(value, name);

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void SetOverloads_SameValue_DoNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(default(bool), nameof(AllTypes.Bool));
            fluentAction.Set(default(byte), nameof(AllTypes.Byte));
            fluentAction.Set(default(sbyte), nameof(AllTypes.SByte));
            fluentAction.Set(default(char), nameof(AllTypes.Char));
            fluentAction.Set(default(decimal), nameof(AllTypes.Decimal));
            fluentAction.Set(default(double), nameof(AllTypes.Double));
            fluentAction.Set(default(float), nameof(AllTypes.Float));
            fluentAction.Set(default(short), nameof(AllTypes.Short));
            fluentAction.Set(default(ushort), nameof(AllTypes.UShort));
            fluentAction.Set(default(int), nameof(AllTypes.Int));
            fluentAction.Set(default(uint), nameof(AllTypes.UInt));
            fluentAction.Set(default(long), nameof(AllTypes.Long));
            fluentAction.Set(default(ulong), nameof(AllTypes.ULong));
            fluentAction.Set(default(string), nameof(AllTypes.String));
            fluentAction.Set(default(DateTime), nameof(AllTypes.DateTime));

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
        public void Set_NoPropertyOfName_DoesNotThrow<T>(T value, string name = "not existing")
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Set(value, name))
                         .Should()
                         .NotThrow();
        }

        [Theory]
        [MemberData(nameof(TestDataSource.CommandsWithRaiseCanExecuteChanged), MemberType = typeof(TestDataSource))]
        public void AffectsCommand_WithRaiseCanExecuteChanged_DoesNothing(ICommand command)
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel.When(false) as IDependencyExpression; // yields an EmptyFluentAction

            EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

            // Act
            fluentAction.Affects(command);

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void AffectsCommand_WithoutRaiseCanExecuteChanged_DoesNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel.When(false) as IDependencyExpression; // yields an EmptyFluentAction

            ICommand command = new TestICommandNoRaiseMethod();

            EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

            // Act
            fluentAction.Affects(command);

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void AffectsCommand_Null_DoesNotThrow()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel.When(false) as IDependencyExpression; // yields an EmptyFluentAction

            ICommand command = null;

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects(command)).Should().NotThrow();
        }

        [Fact]
        public void AffectsProperty_ValidProperty_DoesNotRaiseEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel.When(false) as IDependencyExpression; // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Affects(nameof(TestViewModel.Bool));

            // Assert
            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void AffectsProperty_NotPropertyOfName_DoesNotThrow()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel(Substitute.For<IBackingFieldProvider>());
            IDependencyExpression fluentAction = viewModel.When(false) as IDependencyExpression; // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects("X")).Should().NotThrow();

            listener.Received.Count.Should().Be(0);
        }
    }
}