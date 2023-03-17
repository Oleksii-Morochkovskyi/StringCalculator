using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringCalculatorWorker calculator = new StringCalculatorWorker();

            Console.WriteLine("Enter your string: ");
            string input = Console.ReadLine();
            
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
