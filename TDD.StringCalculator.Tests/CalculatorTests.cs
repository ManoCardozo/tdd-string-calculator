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
    }
}