using System;
using System.Windows.Input;

using FluentAssertions;

using FluentMvvm.Fluent;
using FluentMvvm.Tests.Models;
using FluentMvvm.Tests.Util;

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

        [Fact]
        public void Set_DifferentValue_DoesNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(fluentAction);

            // Act
            fluentAction.Set(42, nameof(viewModel.Id));
            fluentAction.Set("Hello", nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(0);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be(String.Empty);

            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void Set_SameValue_DoesNothing()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel { Id = 0, Name = String.Empty };
            IPropertySetExpression fluentAction = viewModel.When(false); // yields an EmptyFluentAction

            EventListener<string> listener = EventListener<string>.Create(fluentAction);

            // Act
            fluentAction.Set(viewModel.Id, nameof(viewModel.Id));
            fluentAction.Set(viewModel.Name, nameof(viewModel.Name));

            // Assert
            viewModel.Get<int>(nameof(viewModel.Id)).Should().Be(0);
            viewModel.Get<string>(nameof(viewModel.Name)).Should().Be(String.Empty);

            listener.Received.Count.Should().Be(0);
        }

        [Fact]
        public void AffectsCommand_DoesNothing()
        {
            // Arrange
            EmptyFluentAction fluentAction = new EmptyFluentAction();

            ICommand[] commands = { new TestICommandNoRaiseMethod(), new TestICommand(), new TestIWpfCommand() };

            foreach (ICommand command in commands)
            {
                EventListener<EventArgs> listener = EventListener<EventArgs>.Create(command);

                // Act
                fluentAction.Affects(command);

                // Assert
                listener.Received.Count.Should().Be(0);
            }
        }

        [Fact]
        public void AffectsProperty_DoesNotRaiseEvent()
        {
            // Arrange
            EmptyFluentAction fluentAction = new EmptyFluentAction();

            EventListener<string> listener = EventListener<string>.Create(fluentAction);

            // Act
            fluentAction.Affects("X");

            // Assert
            listener.Received.Count.Should().Be(0);
        }
    }
}