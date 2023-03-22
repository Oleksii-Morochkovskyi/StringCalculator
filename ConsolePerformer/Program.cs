namespace ConsolePerformer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var writer = new ConsoleWriter();

            var console = new ConsoleProcessor(writer);

            console.StartCalculator();
        }
    }
}
