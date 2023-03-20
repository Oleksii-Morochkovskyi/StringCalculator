using System;


namespace StringCalculator
{
    public class ConsoleWriter
    {
        public virtual void Print()
        {
            var calculator = new StringCalculatorWorker();
           
            while (true)
            {
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
}
