using System;

namespace ConsolePerformer
{
    public class ConsoleWriter
    {
        public virtual void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        } 
    }
}
