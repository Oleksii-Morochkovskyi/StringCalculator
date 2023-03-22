using System;
using NUnit.Framework;
using Moq;
using StringCalculator;

namespace ConsolePerformer.Tests
{
    [TestFixture]
    public class ConsoleProcessorTests
    {
        private ConsoleProcessor _processor;
        private Mock<StringCalculatorWorker> _calculatorMock;
        private Mock<ConsoleWriter> _writerMock;

        [SetUp]
        public void Setup()
        {
            _writerMock = new Mock<ConsoleWriter>();
            _processor = new ConsoleProcessor(_writerMock.Object);
            _calculatorMock = new Mock<StringCalculatorWorker>();
        }

        [Test]
        public void StartCalculator_ValidInput_ShouldPrintString()
        {
            //Arrange
            var input = "1,2,3";
            _writerMock.SetupSequence(x => x.ReadLine())
                .Returns(input)
                .Returns(string.Empty);
            _calculatorMock.Setup(x => x.Add("1,2,3")).Returns(6);

            //Act
            _processor.StartCalculator();

            //Assert
            _writerMock.Verify(p => p.WriteLine("Result: 6"), Times.Once);
            _writerMock.Verify(p => p.WriteLine("Result: 0"), Times.Once);

        }

        [Test]
        public void StartCalculator_EmptyString_ShouldPrintString()
        {
            //Arrange
            _writerMock.Setup(x => x.ReadLine()).Returns(string.Empty);
            _calculatorMock.Setup(x => x.Add(string.Empty)).Returns(0);

            //Act
            _processor.StartCalculator();

            //Assert
            _writerMock.Verify(p => p.WriteLine("Result: 0"), Times.Once);
        }

        [Test]
        public void StartCalculator_StringWithNegativeNumbers_ShouldThrowException()
        {
            //Arrange
            var input = "1,-2,3";
            _writerMock.SetupSequence(x => x.ReadLine())
                .Returns(input)
                .Returns(string.Empty);
            _calculatorMock.Setup(x => x.Add("1,-2,3")).Throws<Exception>();

            //Act
            _processor.StartCalculator();

            //Assert
            _writerMock.Verify(p => p.WriteLine("Negatives are not allowed: -2"), Times.Once);
            _writerMock.Verify(p => p.WriteLine("Result: 0"), Times.Once);
        }
    }
}
