using Xunit;
using FluentAssertions;
using TDD.StringCalculator.Core;
using System;

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

        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;2,3\n4", 10)]
        public void Add_WithCustomDelimiterAddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("//;\n-1;2", "-1")]
        [InlineData("//;\n1;-2,-3\n4", "-2,-3")]
        [InlineData("-3,-2\n-5", "-3,-2,-5")]
        [InlineData("-5,-80,-5,10,-60", "-5,-80,-5,-60")]
        public void Add_WithNegatives_ThrowsInvalidOperationException(string numbers, string expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            Action action = () => { sut.Add(numbers); };

            // Assert
            action.Should().Throw<InvalidOperationException>().WithMessage($"Negatives not allowed - {expected}");
        }

        [Theory]
        [InlineData("2,1001", 2)]
        [InlineData("5,8000,5,1001", 10)]
        [InlineData("2500\n4,5,1001,90", 99)]
        [InlineData("//;\n1;2000,3\n4", 8)]
        public void Add_AddsUpToAnyNumberAndIgnoresNumbersBiggerThan1000_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[DELIMITER]\n1DELIMITER2,3", 6)]
        [InlineData("//[del]\n1del2del3del4", 10)]
        public void Add_WithSizedCustomDelimiterAddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("//[*][%]\n1*2%3", 6)]
        [InlineData("//[-][;]\n1-2;3,4", 10)]
        public void Add_WithMultipleCustomDelimiterAddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("//[*][%]\n1*2%3", 6)]
        [InlineData("//[***][%%]\n1***2,3%%4", 10)]
        public void Add_WithMultipleSizedCustomDelimiterAddsUpToAnyNumbers_WhenGivenValidString(string numbers, int expected)
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