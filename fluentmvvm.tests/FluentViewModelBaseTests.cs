using System;

using FluentAssertions;

using FluentMvvm.Fluent;
using FluentMvvm.Tests.Models;
using FluentMvvm.Tests.Util;

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
        public void When_False_ReturnsEmptyFluentAction()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            viewModel.When(false).Should().BeOfType<EmptyFluentAction>();
            viewModel.When(() => false).Should().BeOfType<EmptyFluentAction>();
        }

        [Fact]
        public void When_True_ReturnsFluentAction()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            viewModel.When(true).Should().BeOfType<FluentAction>();
            viewModel.When(() => true).Should().BeOfType<FluentAction>();
        }

        [Fact]
        public void When_FuncNull_Throws()
        {
            // Arrange
            TestViewModel viewModel = new TestViewModel();

            // Act & Assert
            FluentActions.Invoking(() => viewModel.When(null)).Should().Throw<ArgumentNullException>();
        }
    }
}