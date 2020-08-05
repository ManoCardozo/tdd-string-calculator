using System;
using System.Linq;
using System.Collections.Generic;

namespace TDD.StringCalculator.Core
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new List<string>() { ",", "\n" };
            var delimiterModifier = "//";
            
            if (numbers.StartsWith(delimiterModifier))
            {
                var splitOnFirstLine = numbers.Split('\n', 2);
                var customDelimiter = splitOnFirstLine[0].Replace(delimiterModifier, string.Empty);

                if (customDelimiter.StartsWith("[") && customDelimiter.EndsWith("]"))
                {
                    customDelimiter = customDelimiter[1..^1];
                }

                delimiters.Add(customDelimiter);
                numbers = splitOnFirstLine[1];
            }

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
    }
}
