using System;
using System.Linq;
using System.Collections.Generic;

namespace TDD.StringCalculator.Core
{
    public class Calculator
    {
        private const string DelimiterModifier = "//";

        public int Add(string input)
        {
            var delimiters = GetDelimiters(input);
            var numbers = GetNumbers(input);

            var threshold = 1000;
            var splitNumbers = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n < threshold);

            if (splitNumbers.Any(n => n < 0))
            {
                throw new InvalidOperationException($"Negatives not allowed - {string.Join(",", splitNumbers.Where(n => n < 0))}");
            }

            return splitNumbers.Sum();
        }

        private string GetNumbers(string input)
        {
            if (input.StartsWith(DelimiterModifier))
            {
                var splitOnFirstLine = input.Split('\n', 2);
                return splitOnFirstLine[1];
            }

            return input;
        }

        private List<string> GetDelimiters(string numbers)
        {
            var delimiters = new List<string>() { ",", "\n" };

            if (numbers.StartsWith(DelimiterModifier))
            {
                var splitOnFirstLine = numbers.Split('\n', 2);
                var customDelimiters = splitOnFirstLine[0].Replace(DelimiterModifier, string.Empty);

                var delimiterCount = customDelimiters.Count(c => c == ']');
                if (delimiterCount > 0)
                {
                    for (int i = 0; i < delimiterCount; i++)
                    {
                        var customDelimiter = customDelimiters[1..customDelimiters.IndexOf("]")];
                        delimiters.Add(customDelimiter);
                        customDelimiters = customDelimiters.Substring(customDelimiters.IndexOf("]") + 1);
                    }
                }
                else
                {
                    delimiters.Add(customDelimiters);
                }

                numbers = splitOnFirstLine[1];
            }

            return delimiters;
        }
    }
}
