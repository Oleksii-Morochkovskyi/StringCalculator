using System;


namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringCalculatorWorker calc = new StringCalculatorWorker();

            Console.WriteLine("Enter your string: ");
            string str = Console.ReadLine();

            try
            {
                Console.WriteLine("Result: " + calc.Add(str));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
