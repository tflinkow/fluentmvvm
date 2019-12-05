using System;

using FluentAssertions;

using FluentMvvm.Fluent;
using FluentMvvm.Tests.Models;
using FluentMvvm.Tests.Util;

using Microsoft.CSharp.RuntimeBinder;

using Xunit;

namespace FluentMvvm.Tests
{
    public sealed class FluentActionTests
    {
        [Fact]
        public void WasUpdated_IsTrue()
        {
            // Arrange
            FluentAction fluentAction = new FluentAction(null, (s => {}));

            // Act & Assert
            fluentAction.WasUpdated().Should().BeTrue();
        }

        [Fact]
        public void AffectsCommand_IWpfCommand_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentAction
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
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentAction
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
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentAction
            TestICommandNoRaiseMethod command = new TestICommandNoRaiseMethod();

            // Act & Assert
            FluentActions.Invoking(() => fluentAction.Affects(command)).Should().Throw<RuntimeBinderException>();
        }

        [Fact]
        public void AffectsProperty_RaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IDependencyExpression fluentAction = viewModel.When(true) as IDependencyExpression; // yields a FluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Affects("X");

            // Assert
            listener.Received.Count.Should().Be(1);
            listener.Received[0].Should().Be("X");
        }

        [Fact]
        public void Set_DifferentValue_SetsAndRaisesEvent()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IPropertySetExpression fluentAction = viewModel.When(true); // yields a FluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(42, nameof(viewModel.Id));
            fluentAction.Set("Hello", nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(42);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be("Hello");

            listener.Received.Count.Should().Be(2);
        }

        [Fact]
        public void Set_SameValue_DoesNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IPropertySetExpression fluentAction = viewModel.When(true); // yields a FluentAction

            EventListener<string> listener = EventListener<string>.Create(viewModel);

            // Act
            fluentAction.Set(viewModel.Id, nameof(viewModel.Id));
            fluentAction.Set(viewModel.Name, nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(0);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be(String.Empty);

            listener.Received.Count.Should().Be(0);
        }
    }
}