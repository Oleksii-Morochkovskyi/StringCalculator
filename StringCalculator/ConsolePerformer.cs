using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class ConsolePerformer
    {
        public void Print()
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
