using System;
using System.Linq;
using System.Collections.Generic;

namespace TDD.StringCalculator.Core
{
    public class Calculator
    {
        private const string DelimiterModifier = "//";
        private const int Threshold = 1000;

        public int Add(string input)
        {
            SplitDelimitersFromNumbers(input, out string numbers, out string customDelimiters);

            var delimiters = GetDelimiters(customDelimiters);

            var splitNumbers = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n < Threshold);

            if (splitNumbers.Any(n => n < 0))
            {
                throw new InvalidOperationException($"Negatives not allowed - {string.Join(",", splitNumbers.Where(n => n < 0))}");
            }

            return splitNumbers.Sum();
        }

        private void SplitDelimitersFromNumbers(string input, out string numbers, out string customDelimiters)
        {
            if (input.StartsWith(DelimiterModifier))
            {
                var splitOnFirstLine = input.Split('\n', 2);
                customDelimiters = splitOnFirstLine[0];
                numbers = splitOnFirstLine[1];
            }
            else
            {
                customDelimiters = string.Empty;
                numbers = input;
            }
        }

        private List<string> GetDelimiters(string customDelimitersInput)
        {
            var delimiters = new List<string>() { ",", "\n" };

            if (customDelimitersInput.StartsWith(DelimiterModifier))
            {
                var customDelimiters = customDelimitersInput.Replace(DelimiterModifier, string.Empty);

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
            }

            return delimiters;
        }
    }
}
