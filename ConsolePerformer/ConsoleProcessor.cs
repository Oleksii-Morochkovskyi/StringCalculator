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
            while (true)
            {
                _writer.WriteLine("Enter your string: (Enter to exit)");

                var input = _writer.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    _writer.WriteLine("Result: " + _calculator.Add(input));

                    break;
                }

                try
                {
                    _writer.WriteLine("Result: " + _calculator.Add(input));
                }
                catch (Exception exception)
                {
                    _writer.WriteLine(exception.Message);
                }
            }

        }
    }

}
