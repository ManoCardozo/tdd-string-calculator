using Xunit;
using FluentAssertions;
using TDD.StringCalculator.Core;

namespace TDD.StringCalculator.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void Add_AddsUpToTwoNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("3", 3)]
        [InlineData("90,20", 110)]
        [InlineData("2,2,6", 10)]
        [InlineData("2,2,6,9", 19)]
        [InlineData("5,80,5,10,60", 160)]
        public void Add_AddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("3,2\n5", 10)]
        public void Add_WithNewLineDelimiterAddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }
    }
}