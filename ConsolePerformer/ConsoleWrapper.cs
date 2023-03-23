using System;

namespace ConsolePerformer
{
    public class ConsoleWrapper
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
