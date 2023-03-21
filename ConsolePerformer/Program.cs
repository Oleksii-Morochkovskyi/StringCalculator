using System;


namespace ConsolePerformer
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var writer = new ConsoleWriter();
            
            var console = new ConsoleProcessor(writer);
            
            console.StartCalculator();
        }
    }
}
