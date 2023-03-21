using System;
using StringCalculator;


namespace ConsolePerformer
{
    public class ConsoleWriter : IConsoleIO
    {
        private readonly IConsoleIO _consoleIO;
        public ConsoleWriter(IConsoleIO consoleIo)
        {
            _consoleIO = consoleIo;
        }
        public void Print()
        {
            _consoleIO.WriteLine("Hello");
            //var output = _consoleIO.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        } 
    }
}
