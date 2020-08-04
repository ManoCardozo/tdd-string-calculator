using System;
using TDD.StringCalculator.Core;

namespace TDD.StringCalculator.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter numbers to add:");
            var input = Console.ReadLine();

            var calculator = new Calculator();
            var result = calculator.Add(input);

            Console.WriteLine($"Result: {result}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
