using System;
using System.Linq;

namespace TDD.StringCalculator.Core
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new[] { ',', '\n' };

            var splitNumbers = numbers
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            if (!splitNumbers.Any())
            {
                return 0;
            }

            return splitNumbers.Sum();
        }
    }
}
