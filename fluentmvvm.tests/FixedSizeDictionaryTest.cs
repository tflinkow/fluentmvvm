using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentMvvm.Internals;
using Shouldly;
using Xunit;

namespace FluentMvvm.Tests
{
    public class FixedSizeDictionaryTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 4)]
        [InlineData(3, 8)]
        [InlineData(4, 8)]
        [InlineData(5, 8)]
        [InlineData(6, 16)]
        [InlineData(10, 16)]
        [InlineData(15, 32)]
        [InlineData(30, 64)]
        public void CalculateCapacity_ReturnsCorrectValue(int size, int expected)
        {
            FixedSizeDictionary<int>.CalculateCapacity(size).ShouldBe(expected);
        }
        
        [Fact]
        public void Create_DuplicateKeys_Throws()
        {
            // Arrange
            IEnumerable<int> source = new Fixture().CreateMany<int>();

            // Act & Assert
            InvalidOperationException exception = Should.Throw<InvalidOperationException>(() => FixedSizeDictionary<int>.Create(source, _ => "Duplicate", x => x));
            exception.Message.ShouldContain("same key");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [AutoData]
        public void Create_IncreasesCountCorrectly(int elementsCount)
        {
            // Arrange
            KeyValuePair<string, int>[] source = new Fixture { RepeatCount = elementsCount }.CreateMany<KeyValuePair<string, int>>().Distinct().ToArray();

            // Act
            FixedSizeDictionary<int> dictionary = FixedSizeDictionary<int>.Create(source, x => x.Key, x => x.Value);

            // Assert
            dictionary.Count.ShouldBe(source.Length);
        }

        [Theory]
        [InlineData(1)]
        [AutoData]
        public void TryGetEntry_ExistingKey_ShouldReturnEntryAndTrue(int elementsCount)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = elementsCount };
            KeyValuePair<string, int>[] source = fixture.CreateMany<KeyValuePair<string, int>>().Distinct().ToArray();

            var keyValuePair = elementsCount is 0 ? fixture.Create<KeyValuePair<string, int>>() : source.RandomElement();

            FixedSizeDictionary<int> dictionary = FixedSizeDictionary<int>.Create(source, x => x.Key, x => x.Value);

            // Act
            bool value = dictionary.TryGetEntry(keyValuePair.Key, out FixedSizeDictionary<int>.Entry entry);

            // Assert
            value.ShouldBeTrue();
            entry.Key.ShouldBe(keyValuePair.Key);
            entry.Value.ShouldBe(keyValuePair.Value);
        }

        [Fact]
        public void TryGetEntry_Empty_ShouldReturnFalse()
        {
            // Arrange
            KeyValuePair<string, int>[] source = Array.Empty<KeyValuePair<string, int>>();

            FixedSizeDictionary<int> dictionary = FixedSizeDictionary<int>.Create(source, x => x.Key, x => x.Value);

            // Act
            bool value = dictionary.TryGetEntry(new Fixture().Create<string>(), out FixedSizeDictionary<int>.Entry entry);

            // Assert
            value.ShouldBeFalse();
            entry.ShouldBe(default);
        }

        [Theory]
        [InlineData(1)]
        [AutoData]
        public void TryGetEntry_NotExistingKey_ShouldReturnFalse(int elementsCount)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = elementsCount + 1 };
            KeyValuePair<string, int>[] source = fixture.CreateMany<KeyValuePair<string, int>>().Distinct().ToArray();
            FixedSizeDictionary<int> dictionary = FixedSizeDictionary<int>.Create(source.Take(source.Length - 1), x => x.Key, x => x.Value);

            // Act
            bool value = dictionary.TryGetEntry(source.Last().Key, out FixedSizeDictionary<int>.Entry outValue);

            // Assert
            value.ShouldBeFalse();
            outValue.ShouldBe(default);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetEntry_NullEmptyOrWhiteSpaceKey_ReturnsFalse(string key)
        {
            // Arrange
            var fixture = new Fixture();
            KeyValuePair<string, int>[] source = fixture.CreateMany<KeyValuePair<string, int>>().Distinct().ToArray();
            FixedSizeDictionary<int> dictionary = FixedSizeDictionary<int>.Create(source, x => x.Key, x => x.Value);

            // Act & Assert
            var value = dictionary.TryGetEntry(key, out FixedSizeDictionary<int>.Entry outValue);

            // Assert
            value.ShouldBeFalse();
            outValue.ShouldBe(default);
        }
    }
}