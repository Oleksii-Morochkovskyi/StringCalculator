using System;

namespace StringCalculator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var calculator = new StringCalculatorWorker();

            Console.WriteLine("Enter your string: ");
            var input = Console.ReadLine();

            try
            {
                Console.WriteLine("Result: " + calculator.Add(input));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
