using System;

using FluentAssertions;

using FluentMvvm.Fluent;
using FluentMvvm.Tests.Models;
using FluentMvvm.Tests.Util;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class FluentViewModelBaseTests
    {
        [Fact]
        public void Get_InitializedValues_ReturnsValues()
        {
            // Arrange & Act
            TestViewModel viewModel = new TestViewModel
            {
                Id = 42,
                Name = "Alice Bob"
            };

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(42);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be("Alice Bob");
        }

        [Fact]
        public void Get_NotInitializedValues_ReturnsDefault()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(default);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be(default);
        }

        [Fact]
        public void Get_NoBackingFieldsProvider_Throws()
        {
            // Arrange
            EmptyTestViewModel viewModel = new EmptyTestViewModel();

            // Act & Assert
            FluentActions.Invoking(() => viewModel.Get<int>("X")).Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void Set_DifferentValue_SetsAndRaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(42, nameof(viewModel.Id));
            viewModel.Set("Alice Bob", nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(42);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be("Alice Bob");

            listener.Received.Count.Should().Be(2);
            listener.Received.Should().BeEquivalentTo(nameof(TestViewModel.Id), nameof(TestViewModel.Name));
        }

        [Fact]
        public void Set_SameValue_DoesNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            viewModel.Set(viewModel.Id, nameof(viewModel.Id));
            viewModel.Set(viewModel.Name, nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(0);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be(String.Empty);

            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void Set_NoBackingFieldsProvider_Throws()
        {
            // Arrange
            EmptyTestViewModel viewModel = new EmptyTestViewModel();

            // Act & Assert
            FluentActions.Invoking(() => viewModel.Set(42, "X")).Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void When_False_ReturnsEmptyFluentAction()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            viewModel.When(false).Should().BeOfType<EmptyFluentAction>();
            viewModel.When(() => false).Should().BeOfType<EmptyFluentAction>();
        }

        [Fact]
        public void When_True_ReturnsSelf()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

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
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            FluentActions.Invoking(() => viewModel.When(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WasUpdated_IsTrue()
        {
            // Arrange
            IDependencyExpression viewModel = new TestViewModel();

            // Act & Assert
            viewModel.WasUpdated().Should().BeTrue();
        }

        [Fact]
        public void AffectsCommand_IWpfCommand_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentViewModelBase
            TestIWpfCommand command = new TestIWpfCommand();

            EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

            // Act
            fluentAction.Affects(command);

            // Assert
            listener.Received.Count.Should().Be(1);
        }

        [Fact]
        public void AffectsCommand_ICommand_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentViewModelBase
            TestICommand command = new TestICommand();

            EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

            // Act
            fluentAction.Affects(command);

            // Assert
            listener.Received.Count.Should().Be(1);
        }

        [Fact]
        public void AffectsCommand_MethodNotExisting_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentViewModelBase
            TestICommandNoRaiseMethod command = new TestICommandNoRaiseMethod();

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects(command)).Should().Throw<RuntimeBinderException>();
        }

        [Fact]
        public void AffectsProperty_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentViewModelBase

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Affects("X");

            // Assert
            listener.Received.Count.Should().Be(1);
            listener.Received[0].Should().Be("X");
        }
    }
}