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

        public void StartCalculator()
        {
            _writer.WriteLine("Enter comma separated numbers (Enter to exit): ");
            
            while (true)
            {
                var input = _writer.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    _writer.WriteLine("\nResult: 0");

                    return;
                }

                PrintSum(input);
                
                _writer.WriteLine("\nYou can enter other numbers (enter to exit)?");
            }
        }

        public void PrintSum(string numbers)
        {
            try
            {
                _writer.WriteLine("\nResult: " + _calculator.Add(numbers));
            }
            catch (Exception exception)
            {
                _writer.WriteLine(exception.Message);
            }
        }
    }

}
