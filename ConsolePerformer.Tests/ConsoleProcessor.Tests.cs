using System;
using NUnit.Framework;
using Moq;
using StringCalculator;

namespace ConsolePerformer.Tests
{
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
        public void StartCalculator_StringWithNumbersThenEmptyString_ShouldPrintSumAndFinishProgram()
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
            _writerMock.Verify(p => p.WriteLine("\nResult: 6"), Times.Once);
            _writerMock.Verify(p => p.WriteLine("\nResult: 0"), Times.Once);

        }

        [Test]
        public void StartCalculator_StringWithNegativeNumbers_ShouldPrintMessageWithFoundNegativeNumbers()
        {
            //Arrange
            var input = "1,-2,3";

            _writerMock.SetupSequence(x => x.ReadLine()).Returns(input);
            _calculatorMock.Setup(x => x.Add("1,-2,3")).Throws<Exception>();

            //Act
            _processor.StartCalculator();

            //Assert
            _writerMock.Verify(p => p.WriteLine("Negatives are not allowed: -2"), Times.Once);
        }

        [Test]
        public void StartCalculator_StringsWithNumbers_ShouldPrintCorrectMessages()
        {
            //Arrange
            var input = "1,2,3";

            _writerMock.SetupSequence(x => x.ReadLine())
                .Returns(input)
                .Returns(string.Empty);

            //Act
            _processor.StartCalculator();

            //Assert
            _writerMock.Verify(p => p.WriteLine("Enter comma separated numbers (Enter to exit): "), Times.Once);
            _writerMock.Verify(p => p.WriteLine("\nYou can enter other numbers (enter to exit)?"), Times.Once);
        }
    }
}
