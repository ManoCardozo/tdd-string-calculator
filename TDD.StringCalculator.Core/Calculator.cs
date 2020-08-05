using System;
using System.Linq;
using System.Collections.Generic;

namespace TDD.StringCalculator.Core
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new List<char>() { ',', '\n' };
            var delimiterModifier = "//";

            if (numbers.StartsWith(delimiterModifier))
            {
                var splitOnFirstLine = numbers.Split('\n', 2);
                var customDelimiter = splitOnFirstLine[0].Replace(delimiterModifier, string.Empty).Single();
                delimiters.Add(customDelimiter);
                numbers = splitOnFirstLine[1];
            }

            var splitNumbers = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            if (splitNumbers.Any(n => n < 0))
            {
                throw new InvalidOperationException($"Negatives not allowed - {string.Join(",", splitNumbers.Where(n => n < 0))}");
            }

            return splitNumbers.Sum();
        }
    }
}
