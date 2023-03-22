using System;
using StringCalculator;

namespace ConsolePerformer
{
    public class ConsoleProcessor
    {
        private readonly ConsoleWriter _writer;

        public ConsoleProcessor(ConsoleWriter writer)
        {
            _writer = writer;
        }

        public virtual void StartCalculator()
        {
            var calculator = new StringCalculatorWorker();

            while (true)
            {
                _writer.WriteLine("Enter your string: (Enter to exit)");
                var input = _writer.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    _writer.WriteLine("Result: " + calculator.Add(input));

                    break;
                }

                try
                {
                    _writer.WriteLine("Result: " + calculator.Add(input));
                }
                catch (Exception exception)
                {
                    _writer.WriteLine(exception.Message);
                }
            }

        }
    }

}
