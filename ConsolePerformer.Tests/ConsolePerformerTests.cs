using NUnit.Framework;
using Moq;
using ConsolePerformer;

namespace ConsolePerformer.Tests
{
    public class ConsolePerformerTests
    {
        private Mock<ConsoleWriter> _writerMock;

        [SetUp]
        public void Setup()
        {
            _writerMock = new Mock<ConsoleWriter>();
        }

        [Test]
        public void WriteLine_ShouldPrintString_IfStringIsNotEmpty()
        {
            _writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));
            var message = "Hello";

            _writerMock.Object.WriteLine(message);

            _writerMock.Verify(p => p.WriteLine(message),Times.Once);
            
        }

        [Test]
        public void Report_ShouldReturnString_IfStringIsNotEmpty()
        {
            _writerMock.Setup(x => x.ReadLine()).Returns("AAA");

            _writerMock.Object.ReadLine();

            _writerMock.Verify(p => p.ReadLine(), Times.Once);

        }
    }
}