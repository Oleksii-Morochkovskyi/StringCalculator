using NUnit.Framework;
using Moq;
using ConsolePerformer;

namespace ConsolePerformer.Tests
{
    public class ConsolePerformerTests
    {
        private ConsoleWriter _printer;
        private Mock<IConsoleIO> _writerMock;
        
        [SetUp]
        public void Setup()
        {
            _writerMock = new Mock<IConsoleIO>();
            
        }

        [Test]
        public void Print_ShouldReturnString_IfStringIsNotEmpty()
        {
            _writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));
            _printer = new ConsoleWriter(_writerMock.Object);
            
            _printer.WriteLine("Hello");

            _writerMock.Verify(t => t.WriteLine("Hello"), Times.Once());
        }
    }
}