using System;
using StringCalculator;

namespace ConsolePerformer
{
    public class ConsoleProcessor
    {
        private readonly ConsoleWriter _writer;
        private readonly StringCalculatorWorker _calculator;

        public ConsoleProcessor(ConsoleWriter writer)
        {
            _writer = writer;
            _calculator = new StringCalculatorWorker();
        }

        public virtual void StartCalculator()
        {
            _writer.WriteLine("Enter comma separated numbers (Enter to exit): ");
            
            while (true)
            {
                var input = _writer.ReadLine();

                PrintSum(input);
                
                _writer.WriteLine("\nYou can enter other numbers (enter to exit)?");
            }
        }

        public void PrintSum(string numbers)
        {
            try
            {
                _writer.WriteLine("\nResult: " + _calculator.Add(numbers));

                if (string.IsNullOrEmpty(numbers))
                {
                    System.Environment.Exit(1);
                }
            }
            catch (Exception exception)
            {
                _writer.WriteLine(exception.Message);
            }
        }
    }

}
